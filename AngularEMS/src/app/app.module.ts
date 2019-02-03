import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AgmCoreModule } from '@agm/core';
import { NgxPaginationModule } from 'ngx-pagination';
import { AngularFireModule } from 'angularfire2';
import { AngularFirestoreModule } from 'angularfire2/firestore';
import { AngularFireAuthModule } from 'angularfire2/auth';
import 'hammerjs';
import 'mousetrap';
import { ModalGalleryModule } from '@ks89/angular-modal-gallery';
import { environment } from '../environments/environment';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { NgxLoadingModule } from 'ngx-loading';
import { AgmDirectionModule } from 'agm-direction';
import { SidebarModule } from 'ng-sidebar';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { SearchComponent } from './components/search/search.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { ForgotPasswordComponent } from './components/authentication/forgot-password/forgot-password.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { MapComponent } from './components/map/map.component';
import { AnnouncementBoxComponent } from './components/announcement/announcement-box/announcement-box.component';
import { AnnouncementsComponent } from './components/announcement/announcements/announcements.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AnnouncementDetailsComponent } from './components/announcement/announcement-details/announcement-details.component';
import { VerifyEmailComponent } from './components/authentication/verify-email/verify-email.component';
import { ResetPasswordComponent } from './components/authentication/reset-password/reset-password.component';
import { PopupMessageComponent } from './components/popup-message/popup-message.component';
import { ReportIncidentComponent } from './components/incident/report-incident/report-incident.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    SearchComponent,
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    FooterComponent,
    NotFoundComponent,
    MapComponent,
    AnnouncementBoxComponent,
    AnnouncementsComponent,
    UserProfileComponent,
    AnnouncementDetailsComponent,
    VerifyEmailComponent,
    ResetPasswordComponent,
    PopupMessageComponent,
    ReportIncidentComponent
  ],
  imports: [
    ModalGalleryModule.forRoot(),
    AngularFireModule.initializeApp(environment.firebase),
    AngularFirestoreModule, // imports firebase/firestore, only needed for database features
    AngularFireAuthModule, // imports firebase/auth, only needed for auth features
    HttpClientModule,
    BrowserModule,
    DropDownsModule,
    BrowserAnimationsModule,
    NgbModule.forRoot(),
    FormsModule,
    AppRoutingModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCt0ESIUAMdoIqotUXzsAQplKQNrCmoeEw'
    }),
    AgmDirectionModule,
    NgxPaginationModule,
    InputsModule,
    NgxLoadingModule,
    SidebarModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
