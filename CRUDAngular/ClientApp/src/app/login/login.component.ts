import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  loading = false;
  submitted = false;
  baseUrl: string;

  ngOnInit(): void {
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public onSubmit() {

    this.submitted = true;

    this.loading = true;

    let login = {
      Username: document.getElementById('Username')['value'],
      Password: document.getElementById('Password')['value'],
    };

    this.httpClient.post<any>(this.baseUrl + "api/Authenticate/Login", login)
      .subscribe(
        result => {
          alert(result.message || "Success!");
          this.router.navigate(["Products"]);
        }, error => {
          this.loading = false;
          console.error(error);
          alert(error.error || "Was not possible to login.");
        });

  }

}
