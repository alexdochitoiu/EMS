import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { ForgotPasswordComponent } from './components/authentication/forgot-password/forgot-password.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AnnouncementsComponent } from './components/announcement/announcements/announcements.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AnnouncementDetailsComponent } from './components/announcement/announcement-details/announcement-details.component';
import { VerifyEmailComponent } from './components/authentication/verify-email/verify-email.component';
import { ResetPasswordComponent } from './components/authentication/reset-password/reset-password.component';
import { ReportIncidentComponent } from './components/incident/report-incident/report-incident.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'verify/email/:userId/:emailToken',
    component: VerifyEmailComponent
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent
  },
  {
    path: 'reset/password/:userId/:resetPasswordToken',
    component: ResetPasswordComponent
  },
  {
    path: 'announcements',
    component: AnnouncementsComponent
  },
  {
    path: 'announcements/:id',
    component: AnnouncementDetailsComponent
  },
  {
    path: 'users/:username',
    component: UserProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'incidents/report',
    component: ReportIncidentComponent
  },


  { path: '404', component: NotFoundComponent },
  { path: '**', redirectTo: '404' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
