import { Component, HostListener, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter, map } from 'rxjs/operators';
import { SampleImagePageFloatingContentPositionService } from 'src/app/services/sample-image-page-floating-content-position.service';
import { HipaIconService } from './services/hipa-icon/hipa-icon.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'HIPA';

  constructor(
    private sampleImageHelperService: SampleImagePageFloatingContentPositionService,
    private titleService: Title,
    private hipaIconService: HipaIconService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.router.events
      .pipe(
        filter(event => event instanceof NavigationEnd),
        map(() => {
          let child = this.route.firstChild;
          while (child?.firstChild) {
            child = child.firstChild;
          }
          return child?.snapshot.data['title'] || 'HIPA'; // Заголовок по умолчанию
        })
      )
      .subscribe(title => {
        this.titleService.setTitle(title);
      });
    this.hipaIconService.registerIcons();
  }

  @HostListener('window:resize', ['$event'])
  onWindowResize() {
    this.sampleImageHelperService.updateScreenHeight();
  }
}
