import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  loading = false;
  submitted = false;
  baseUrl: string;
  products: any;

  ngOnInit(): void {
    this.httpClient.get<any>(this.baseUrl + "api/Products")
      .subscribe(
        result => {
          console.log(result);
          this.products = result;
        }, error => {
          this.loading = false;
          console.error(error);
          alert(error.error || "Was not possible to get products.");
        });
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private  httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


  public onDeleteClick(product) {

    this.loading = true;

    this.httpClient.delete<any>(this.baseUrl + "api/Products/" + product.id)
      .subscribe(
        result => {
          alert("Product deleted.");
          this.ngOnInit();
        }, error => {
          this.loading = false;
          console.error(error);
          alert(error.error || "Was not possible to delete product.");
          this.router.navigate(["Products"]);

        });
  }
}
