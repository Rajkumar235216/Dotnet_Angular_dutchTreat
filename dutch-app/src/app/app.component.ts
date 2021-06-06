import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
     <div style="text-align:center">
      <h1>
        Welcome to {{title}}!
        Raj
      </h1>
      <span style="display: block">{{ title }} app is running!</span>
    </div>
        
  `,
  styles: []
})
export class AppComponent {
  title = 'Dutch Treat';
}
