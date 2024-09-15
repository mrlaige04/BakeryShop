import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncate',
  standalone: true
})
export class TruncatePipe implements PipeTransform {
  transform(text: string, charsCount: number): unknown {
    return text.substring(0, charsCount + 1);
  }
}
