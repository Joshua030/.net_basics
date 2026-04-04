import { Component, effect, ElementRef, inject, OnInit, signal, ViewChild } from '@angular/core';
import { MessageService } from '../../../core/services/message-service';
import { MemberService } from '../../../core/services/member-service';
import { Message } from '../../../types/message';
import { DatePipe } from '@angular/common';
import { TimeAgoPipe } from '../../../core/pipes/time-ago-pipe';
import { FormsModule } from '@angular/forms';
import { PresenceService } from '../../../core/services/presence-service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-messages',
  imports: [DatePipe, TimeAgoPipe, FormsModule],
  templateUrl: './member-messages.html',
  styleUrl: './member-messages.css',
})
export class MemberMessages implements OnInit {
  @ViewChild('messageEndRef') messageEndRef!: ElementRef;
  protected messageService = inject(MessageService);
  private memberService = inject(MemberService);
  protected presenceService = inject(PresenceService);
  private route = inject(ActivatedRoute);
  protected messages = signal<Message[]>([]);
  protected messageContent = '';

  ngOnInit(): void {
    // this.loadMessages();
    this.route.parent?.paramMap.subscribe({
      next: (params) => {
        const otherUserId = params.get('id');
        if (!otherUserId) throw new Error('Cannot connect to hub');
        this.messageService.createHubConnection(otherUserId);
      },
    });
  }

  constructor() {
    effect(() => {
      const currentMessages = this.messages();

      if (currentMessages.length > 0) {
        this.scrollToBottom();
      }
    });
  }

  loadMessages() {
    const memberId = this.memberService.member()?.id;

    if (memberId) {
      this.messageService.getMessagesThread(memberId).subscribe({
        next: (messages) => {
          console.log(messages, 'Fetched Messages:');
          this.messages.set(
            messages.map((message) => ({
              ...message,
              currentUserSender: message.senderId !== memberId,
            })),
          );
        },
        complete: () => this.scrollToBottom(),
      });
    }
  }

  sendMessage() {
    const recipientId = this.memberService.member()?.id;
    if (!recipientId) return;

    const content = this.messageContent;
    this.messageContent = ''; // ✅ clear before the async call

    this.messageService.sendMessage(recipientId, content).subscribe({
      next: (message) => {
        this.messages.update((messages) => [...messages, message]);
      },
    });
  }

  scrollToBottom() {
    setTimeout(() => {
      if (this.messageEndRef) {
        this.messageEndRef.nativeElement.scrollIntoView({ behavior: 'smooth' });
      }
    });
  }
}
