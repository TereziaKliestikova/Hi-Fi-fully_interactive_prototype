import { Injectable } from '@angular/core';

// This service is created to solve issue of multiple floating elements in the sample image page.
// Service is solving this issue by providing the correct position to these elements.
@Injectable({
  providedIn: 'root',
})
export class SampleImagePageFloatingContentPositionService {
  // value is from src/app/app-layout/header/header.component.scss
  private appHeaderHeight = 68;
  // these values are from src/app/app-layout/organ/sample-image/sample-image.component.scss
  private navigatorHeight = 179;
  private navigatorWidth = 259;
  private floatingButtonHeight = 59;
  private floatingButtonBottomOffset = 20;
  private gapBetweenElements = 10;
  private navigatorRightOffset = 13;
  // value is from src/app/app-layout/organ/sample-image/sample-image-helper/sample-image-helper.component.scss
  private assistantPadding = 16;

  // dynamically set from the src/app/app-layout/organ/sample-image/sample-image-helper/sample-image-helper.component.ts
  private screenHeight = window.innerHeight;

  constructor() {}

  updateScreenHeight() {
    this.screenHeight = window.innerHeight;
  }

  // TODO add minimap and floating button style methods

  getAssistantStyle() {
    const topElements =
      this.appHeaderHeight + 2 * this.gapBetweenElements + this.navigatorHeight;
    const bottomElements =
      this.floatingButtonHeight +
      this.floatingButtonBottomOffset +
      this.gapBetweenElements +
      2 * this.assistantPadding;

    return {
      'width.px': this.navigatorWidth,
      'height.px': this.screenHeight - topElements - bottomElements,
      'top.px': topElements,
      'right.px': this.navigatorRightOffset,
    };
  }
}
