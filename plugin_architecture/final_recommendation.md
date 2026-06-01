
What carries over vs. what doesn't

Carries over (the actual "plugin" idea):
- A shared contract assembly that both host and plugins reference (your PlugInBase).
- Reflection-based discovery — the host finds implementations of the contract instead of hard-coding them.
- The principle of decoupling: the host depends on the contract, not on concrete plugins.

Does not carry over (console-specific):
- ICommand with an Invoke() you call in a loop — a web plugin instead contributes endpoints + DI services, which it must register into ASP.NET Core's pipeline at startup.
- The folder-scanning AssemblyLoadContext loader and the plugins/<name>/ isolation dance — that whole layer exists to load DLLs at runtime that weren't there at compile time. For a backend-only Web API, you almost never need that.

So: is the current approach recommended for you?

For backend-only plugins (your answer), the honest take: no, runtime DLL loading is not recommended — it buys you a capability (add features to a running, deployed server without recompiling) that you probably don't need, at a steep cost:

- AssemblyLoadContext + MVC Application Parts wiring to make controllers discoverable,
- dependency/version conflicts between plugins and the host,
- EF Core migration headaches when plugins own their own tables,
- assembly unload edge cases if you ever want hot-reload.

Unless you're building a true marketplace / third-party add-on platform (think nopCommerce, Orchard Core, an app store where other people ship DLLs into your live server), the runtime loader is solving a problem you don't have.

What I'd actually recommend: a modular monolith

Keep the spirit of the plugin pattern but make it build-time:

- A shared Abstractions project with an interface like:
public interface IModule
{
    IServiceCollection RegisterServices(IServiceCollection services);
    IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
}
- Each "plugin" is a normal class library (Modules.Weather, Modules.Billing, …) referenced by the API project. It implements IModule, brings its own controllers/minimal-API routes, services, and EF DbContext if needed.
- At startup the API reflects over loaded assemblies, finds every IModule, and calls RegisterServices + MapEndpoints. Controllers are auto-discovered because they're in referenced assemblies — no Application Parts gymnastics needed.

You get clean separation, type safety, easy debugging, and adding a feature is just "reference a new module project." The only "cost" vs. runtime loading is that adding a module means a redeploy — which for a backend-only app you're doing anyway.

Where Angular fits (backend-only)

Your Angular app stays a single, fixed app. The API exposes a discovery endpoint:

GET /api/modules   →   [{ "key": "billing", "routes": ["/invoices"], "menu": {...}, "permissions": [...] }]

Angular calls it on load and drives its menu/feature flags/routing guards from that manifest — so when you enable a backend module, the relevant UI lights up without hard-coding it in the frontend. The Angular app doesn't load any backend code; it just reacts to "what's turned on."

---
So in one line: same pattern, drop the runtime loader, use referenced modules + an IModule startup hook + a manifest endpoint for Angular.

Want me to keep going on any of these — e.g. how IModule discovery + MapEndpoints looks in Program.cs, how a module owns its own EF migrations, or what the /api/modules manifest + Angular consumption looks like? Happy to whiteboard it before any code.
