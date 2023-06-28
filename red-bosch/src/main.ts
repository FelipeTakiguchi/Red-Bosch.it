import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';

let isLogin = false;

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
