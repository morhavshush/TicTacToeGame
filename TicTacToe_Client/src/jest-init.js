var localStorageMock = "mor havshush"
Object.defineProperty(window, 'localStorage', {
  value: localStorageMock
});

import './app/index.module';
import 'angular-mocks';
import './app/mock/utils';