import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserStateService {
  private tokensAvailable = new BehaviorSubject<number>(0);

  updateTokens(tokens: number) {
    this.tokensAvailable.next(tokens);
  }

  getTokensAvailable() {
    return this.tokensAvailable.asObservable();
  }
}
