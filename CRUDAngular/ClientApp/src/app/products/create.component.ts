import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class ProductCreateComponent implements OnInit {
  loading = false;
  submitted = false;
  baseUrl: string;
  product: any;
  hideProductImage = true;
  fileImage: File = null;
  private routeSub: Subscription;

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      if (params['id']) {
        //get from api
        let id = params['id'];

        this.httpClient.get<any>(this.baseUrl + "api/Products/" + id)
          .subscribe(
            result => {
              //console.log(result);
              this.product = result;
              this.hideProductImage = !this.product.imageUrl;

            }, error => {
              this.loading = false;
              console.error(error);
              alert(error.error || "Was not possible to get product.");
              this.router.navigate(["Products"]);

            });

      } else {
        this.product = {
          name: '',
          id: '',
          price: 0,
        };

        this.hideProductImage = true;
      }
    });
  }

  public onSubmit() {

    this.submitted = true;

    this.loading = true;

    this.product.price = parseFloat(document.getElementById('Price')['value']);
    this.product.name = document.getElementById('Name')['value'];


    if (!this.product.id) {
      this.product.id = 0;
    }

    const formData: FormData = new FormData();
    if (this.fileImage) {
      formData.append('fileImage', this.fileImage, this.fileImage.name);
    }

    for (let p in this.product) {
      formData.append(p, this.product[p]);
    }

    if (this.product.id) {
      //put
      this.httpClient.put<any>(this.baseUrl + "api/Products/" + this.product.id, formData)
        .subscribe(
          result => {
            console.log(result);
            alert("Success!");
            this.router.navigate(["Products"]);
          }, error => {
            this.loading = false;
            console.error(error);
            alert(error.error || "Was not possible to post.");
          });

    } else {
      //post
      this.httpClient.post<any>(this.baseUrl + "api/Products", formData)
        .subscribe(
          result => {
            console.log(result);
            alert("Success!");
            this.router.navigate(["Products"]);
          }, error => {
            this.loading = false;
            console.error(error);
            alert(error.error || "Was not possible to post.");
          });

    }

  }

  fileInput() {
    if (document.getElementById('FileImage')['files']) {
      this.fileImage = document.getElementById('FileImage')['files'][0];
    } else {
      this.fileImage = null;
    }

  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


}
