import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'userName'
})
export class UserNamePipe implements PipeTransform {
  transform(userId: number, users: any[]): string {
    const user = users.find(u => u.UserID === userId);
    return user ? user.Username : 'Unknown User';
  }
}
