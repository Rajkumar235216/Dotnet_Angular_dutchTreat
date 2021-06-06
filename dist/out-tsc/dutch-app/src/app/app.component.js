import { __decorate } from "tslib";
import { Component } from '@angular/core';
let AppComponent = class AppComponent {
    constructor() {
        this.title = 'Dutch Treat';
    }
};
AppComponent = __decorate([
    Component({
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
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map