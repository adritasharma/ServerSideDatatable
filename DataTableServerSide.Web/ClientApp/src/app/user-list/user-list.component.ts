import { Component, ViewChild, OnInit } from '@angular/core';
import { DataTableDirective } from 'angular-datatables';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';


export interface DataTableResponse {
  data: Array<any>;
  draw: number;
  totalRecordsFiltered: number;
  totalRecords: number;
}

@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  constructor(public _http: HttpClient) { }

  dtOptions: DataTables.Settings = {};
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;
  dtInstance: Promise<DataTables.Api>;

  allUsers = [];
  status = "active";
  userUrl = `${environment.apiUrl}user`;


  ngOnInit() {
    const that = this;
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      serverSide: true,
      processing: false,
      ajax: (dataTablesParameters: any, callback) => {
        dataTablesParameters.FilterType = this.status;

        that._http.get(`${this.userUrl}?${this.convertToParam(dataTablesParameters)}`).subscribe((resp: DataTableResponse) => {
          console.log("Result - ", resp);
          this.allUsers = resp.data
          callback({
            recordsTotal: resp.totalRecords,
            recordsFiltered: resp.totalRecordsFiltered,
            data: []
          });
        });
      },
      // "order": [],
      columns: [{ "orderable": false, "searchable": false }, { data: 'fullName' }, { data: 'email' }, { data: 'empid', "orderable": false }, { data: 'roleNames'}]
    };
  }


  reload() {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.ajax.reload()
    });
  }

  ngAfterViewInit(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.columns().every(function () {
        const that = this;
        $('input', this.footer()).on('keyup change', function () {
          if (that.search() !== this['value']) {
            that
              .search(this['value'])
              .draw();
          }
        });
      });
    });
  }

  convertToParam(json: any) {
    return $.param(json);
  }
}
