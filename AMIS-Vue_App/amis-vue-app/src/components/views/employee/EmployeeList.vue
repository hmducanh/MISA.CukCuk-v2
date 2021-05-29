<template>
    <div
    @keydown.shift.65.prevent.stop="btnAddOnClick" 
    tabindex="0"
    >
        <div class = "content-body">
      <div class = "content-title">
          Nhân viên
      </div>
      <!-- button add employee -->
      <div class = "buttonAdd" @click="btnAddOnClick()">
        <button class = "btn-default">Thêm một nhân viên</button>
      </div>
    </div>
    <!-- input search va button refresh -->
    <div class = "content-table">
    <SuccessDialog  :open="isShowSuccessDialog" />
        <div class = "bar">
            <input type="text" class = "search-table" placeholder="Tìm theo mã, tên nhân viên" @input="search_input($event)" v-model="search">
            <div class = "logo-item search-icon"></div>
            <div class = "logo-item refresh-button" @click="refreshOnClick()"></div>
            <div class = "logo-item export-button" @click="exportToExcel()"></div>
        </div>
        <!-- Tao bang data -->
        <div class = "grid">
            <table id = "tblListEmployee" class = "table">
                <thead>
                    <tr class="line0">
                        <th style="width: 46px; text-align: center; position: sticky; left: 0;">
                            <input type="checkbox" id="top-checkbox" @click="topCheckboxOnClick()" style="cursor: pointer;">
                        </th>
                        <th style="min-width: 150px; width: 150px;">MÃ NHÂN VIÊN</th>
                        <th style="min-width: 250px;">TÊN NHÂN VIÊN</th>
                        <th style="min-width: 150px; width: 150px;">GIỚI TÍNH</th>
                        <th style="min-width: 150px; width: 150px; text-align: center;">NGÀY SINH</th>
                        <th style="min-width: 200px; width: 200px;">SỐ CMND</th>
                        <th style="min-width: 250px; width: 250px;">CHỨC DANH</th>
                        <th style="min-width: 250px; width: 250px;">TÊN ĐƠN VỊ</th>
                        <th style="min-width: 150px; width: 150px;">SỐ TÀI KHOẢN</th>
                        <th style="min-width: 250px; width: 250px;">TÊN NGÂN HÀNG</th>
                        <th style="min-width: 250px; width: 250px;">CHI NHÁNH TK NGÂN HÀNG</th>
                        <th style="min-width: 120px; width: 120px;;">CHỨC NĂNG</th>
                    </tr>
                </thead>
                <tbody :class="{ 'hide-body': isLoading}">
                    <!-- get data from api -->
                    <tr v-for="employee in employees" :key='employee.employeeId'>
                        <td><input type="checkbox" v-bind:id="employee.employeeCode" @click="trCheckBoxOnClick(employee.employeeCode)"> </td>
                        <td>{{employee.employeeCode}}</td>
                        <td>{{employee.fullName}}</td>
                        <td>{{employee.genderName}}</td>
                        <td style=" text-align: center;">{{ formatDateDDMMYYYYnew(employee.dateOfBirth) }}</td>
                        <td>{{employee.identifyNumber}}</td>
                        <td>{{employee.positionName}}</td>
                        <td>{{employee.departmentName}}</td>
                        <td>{{employee.bankAccount}}</td>
                        <td>{{employee.bankName}}</td>
                        <td>{{employee.bankBranch}}</td>
                        <td>
                            <!-- <button class="btnDel" @click="btnDelOnClick(employee.employeeId)">Xóa</button> -->
                            <div style="display: flex; margin-left: 30%;">
                                <div class="edit-text" @click="trOnClick(employee.employeeId)" style="cursor: pointer; color: #0075C0; font-weight: 500;">Sửa</div>
                                <a-dropdown :trigger="['click']">
                                    <a>
                                        <a-icon style="margin-left:5px; font-weight:700; width: 20px;" type="down"/>
                                    </a>
                                    <a-menu slot="overlay">
                                        <a-menu-item key="0" @click="replication(employee.employeeId)">
                                            Nhân bản
                                        </a-menu-item>
                                    <a-menu-item @click="btnDelOnClick(employee.employeeId, employee.employeeCode)" key="1">
                                        <a>Xóa</a>
                                    </a-menu-item>
                                    <a-menu-item key="2" class="">
                                        Ngừng sử dụng
                                    </a-menu-item>
                                    </a-menu>
                                </a-dropdown>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="pagging">
            <div class="total-record">Tổng số: <b>{{totalRecords}}</b> bản ghi</div>
             <!-- <button @click="openNotificationWithIcon("success")">Success</button> -->
                <div class="right-pagging">
                    <div class="select-record-number">
                        <a-select default-value="10" style="width: 200px; margin-right: 10px;" @change="handleChangePageSize">
                            <a-select-option value=10>
                            10 bản ghi trên 1 trang
                            </a-select-option>
                            <a-select-option value=20>
                            20 bản ghi trên 1 trang
                            </a-select-option>
                            <a-select-option value=30>
                            30 bản ghi trên 1 trang
                            </a-select-option>
                            <a-select-option value=50>
                            50 bản ghi trên 1 trang
                            </a-select-option>
                            <a-select-option value=100>
                            100 bản ghi trên 1 trang
                            </a-select-option>
                        </a-select>
                    </div>
                <div class="select-page">
                <a-pagination
                    v-model="page"
                    size="small"
                    class="pagging-item"
                    :showLessItems="showLessItems" 
                    :item-render="itemRender"
                    :total="totalRecords" 
                    :pageSize="pageSize" 
                    @change="handleChangePageIndex"
                />
                </div>
            </div>
        </div>
    <!-- icon loader -->
        <div class="icon-loader" :class="{ 'icon-loader-show': !isLoading}"></div>
    <!-- tranfer data to employeedetail -->
        
        <EmployeeDetail
        v-if="isShowDialogDetail"
        @hideDialog="hideDialog()"
        :employee="selectedEmployee"
        :constantEmployee="constantSelectedEmployee"
        :formMode="dialogFormMode"
        :departments="departments"
        @newDialog="newDialog()"
        />
        <!-- warning when click delete button -->
        <WarningDialog
        :isShowWarning="isShowWarningDialog"
        :employeeCode="employeeCode"
        @hideWarningDialog="hideWarningDialog()"
        @status_delete="child_click"
        />
    <!-- Success dialog 
    created by : hmducanh (13/5/2021)
    -->
    </div>
    
    </div>
</template>

<script>
import axios from "axios";
import _lodash from "lodash";
import EmployeeDetail from "../dialog/EmployeeDetail.vue";
import WarningDialog from '../dialog/WarningDialog.vue';
import SuccessDialog from '../dialog/SuccessDialog.vue';


export default {
    components : {
        EmployeeDetail,
        WarningDialog,
        SuccessDialog,
    },
    /* 
    load du lieu luc bat dau vao traang
    created by : hmducanh (9/5/2021)
    */
    created() {
        // block cac to hop phim cua chrome de nhan rieng cac lenh cua dev
        // created by : hmducanh (20/5/2021)
        document.addEventListener("keydown", function (e) {
        e = e || window.event;//Get event
        if (e.ctrlKey) {
            var c = e.which || e.keyCode;//Get key code
            switch (c) {
                case 83: //Block Ctrl+S
                    e.preventDefault();     
                    e.stopPropagation();
                break;
            }
        }
        });
        // lay danh sach nhan vien  
        axios.get("https://localhost:44332/api/v1/Employee/Filter?Page=" + this.page + "&PageSize=" + this.pageSize + "&Search=" + this.search).then(res => {
            this.employees = res.data.data;
            /* this.validEmployee = res.data.data; */
            this.totalRecords = res.data.totalRecords;
            this.totalPage = res.data.totalPages;
            console.log(res);
        }).catch(res => {
            console.log(res);
        });
        // lay danh sach don vi
        axios.get("https://localhost:44332/api/v1/Department").then(res => {
            this.departments = res.data;
            console.log(res);
        }).catch(res => {
            console.log(res);
        });
    },
    props : [],
    methods : {
        /* 
        chay gif xoay xoay khi loading data
        created by : hmducanh (12/5/2021)
        */
        debounceLoadData:_lodash.debounce(function(){
            this.isLoading = false;
        },600),
        /* 
        hien thi dialog thanh cong
        created by : hmducanh (13/5/2021)
        */
        /* debounceShowSuccess:_lodash.debounce(function(){ // khong dung nua
            this.isShowSuccessDialog = true;
            console.log(this.employees);
            this.debounceCloseSuccess();
        },20), */
        /* 
        tat hien thi dialog thanh cong
        created by : hmducanh (13/5/2021)
        */
        /* debounceCloseSuccess:_lodash.debounce(function(){ // khong dung nua
            this.isShowSuccessDialog = false;
        },2000), */
        /* 
        load lai data trong bang
        created by : hmducanh (9/5/2021)
        */
        loadData() {
            // hien gif loading
            this.isLoading = true;  
            // lay data tu api phan trang
            axios.get("https://localhost:44332/api/v1/Employee/Filter?Page=" + this.page + "&PageSize=" + this.pageSize + "&Search=" + this.search).then(res => {
            this.employees = res.data.data;
            /* this.validEmployee = res.data.data; */
            this.totalRecords = res.data.totalRecords;
            this.totalPage = res.data.totalPages;
            console.log(res);
            }).catch(res => {
                console.log(res);
            });
            this.debounceLoadData();
        },
        /* 
        click vao button de add employee
        created by : hmducanh (11/5/2021)
        */
        btnAddOnClick() {
            axios.get("https://localhost:44332/api/v1/Employee/maxEmployeeCode").then(res => {
                this.selectedEmployee = {};
                this.constantSelectedEmployee = {};
                this.dialogFormMode = "add";
                this.isShowDialogDetail = true;
                this.selectedEmployee.employeeCode = res.data;
                this.constantSelectedEmployee.employeeCode = res.data;
                this.Gender = null;
            }).catch(res => {
                console.log(res);
            });
        },  
        /* 
        // hien thi - an form dialog
        // Created by : hmducanh (10/5/2021)
        */
        hideDialog() {
            //an dialog
            this.isShowDialogDetail = false;
            // load lai data
            this.loadData();
        }, 
        /* 
        kiem tra xem xau 'child' co phai la xau con cua xau 'father' khong 
        dung de filter
        created by : hmducanh (12/5/2021) 
        */
        check_substring(father, child)
        {
            if(child == "" || child == null)
                return true;
            for(let i = 0; i < (father.length - child.length + 1); i++)
            {
                this.check_sub = true;
                for(let j = 0; j < child.length; j++)
                {
                    if(father[i + j] != child[j])
                    {
                        this.check_sub = false;
                        break;
                    }
                }
                if(this.check_sub == true)
                    return true;
            }
            return false;
        },
        /* 
        lien tuc nhan su kien tu input text de loc data
        created by : hmducanh (18/5/2021)
        */
        search_input:_lodash.debounce(function(event){
            this.search = event.target.value;
            this.page = 1;
            this.pageSize = 10;
            this.loadData();
        }, 500),
        /* 
        an vao refresh button
        tro ve trang doi ( trang 1 + 10 nhan vien 1 trang)
        created by : hmducanh (16/5/2021)
        */
        refreshOnClick:_lodash.debounce(function() {
            this.page = 1;
            this.pageSize = 10;
            this.search = "";
            this.loadData();
        }, 400),
        /* 
        an vao se tai ve file excel chua toan bo nhan vien
        created by : hmducanh (18/5/2021)
         */
        exportToExcel() {
            window.open("https://localhost:44332/api/v1/Employee/ExportFileExcel");
        },
        /* 
        click vao check box o trong table -> body -> row
        nhan no va kiem tra xem tat ca checkbox co = true khong
        neu co thi bat check cua header
        created by : hmducanh (12/5/2021)
        */
        trCheckBoxOnClick(id) {
            if(document.querySelector("#" + id).checked == false)
                document.querySelector("#top-checkbox").checked = false;
            else
            {
                for(let index in this.employees)
                {
                    var Id = "#" + `${this.employees[index].employeeCode}`;
                    if(document.querySelector(Id).checked == false)
                    {
                        document.querySelector("#top-checkbox").checked = false;
                        return ;
                    }
                }
                document.querySelector("#top-checkbox").checked = true;
            }
        },
        /* 
        nhan su kien click vao check box cua header
        sau do fill cac checkbox cua cac hang duoi tuong ung
        created by : hmducanh (12/5/2021)
        */
        topCheckboxOnClick() {
            this.topCheckboxstatus = document.querySelector("#top-checkbox").checked;
            for(let index in this.employees)
            {
                var Id = "#" + `${this.employees[index].employeeCode}`;
                document.querySelector(Id).checked = this.topCheckboxstatus;
            }
        },
        /* 
        thuc hien nhan ban 
        tao form moi voi data cua hang hien tai nhung la "add" va employeeCOde = max + 1
        created by : hmducanh (16/5/2021)
        */
        async replication(EmployeeId) {
            await axios.get("https://localhost:44332/api/v1/Employee/maxEmployeeCode").then(res => {
                this.maxEmployeeCode = res.data;
            }).catch(res => {
                console.log(res);
            });
            axios.get("https://localhost:44332/api/v1/Employee/GetById/" + EmployeeId).then(res => {
                this.selectedEmployee = res.data;
                const _dateOfBirth = new Date(this.selectedEmployee.dateOfBirth); // ngay sinh
                    this.selectedEmployee.dateOfBirth =
                    _dateOfBirth.getFullYear().toString() +
                    "-" +
                    (_dateOfBirth.getMonth() + 1 < 10 ? "0" : "") +
                    (_dateOfBirth.getMonth() + 1).toString() +
                    "-" +
                    (_dateOfBirth.getDate() < 10 ? "0" : "") +
                    _dateOfBirth.getDate().toString();
                const _identifyDate = new Date(this.selectedEmployee.identifyDate); // ngay cap cmnd
                    this.selectedEmployee.identifyDate =
                    _identifyDate.getFullYear().toString() +
                    "-" +
                    (_identifyDate.getMonth() + 1 < 10 ? "0" : "") +
                    (_identifyDate.getMonth() + 1).toString() +
                    "-" +
                    (_identifyDate.getDate() < 10 ? "0" : "") +
                    _identifyDate.getDate().toString();               
                this.Gender = parseInt(res.data.gender);
                this.selectedEmployee.employeeCode =  this.maxEmployeeCode;  
                this.dialogFormMode = "add";
                this.isShowDialogDetail = true;
            }).catch(res => {
                console.log(res);
            });
        },
        /* 
        double click vao 1 hang trong bang de edit -> an vao nut sua
        created by : hmducanh (11/5/2021)
        */
        trOnClick(EmployeeId) {
            axios.get("https://localhost:44332/api/v1/Employee/GetById/" + EmployeeId).then(res => {
                this.selectedEmployee = res.data;
                const _dateOfBirth = new Date(this.selectedEmployee.dateOfBirth); // ngay sinh
                    this.selectedEmployee.dateOfBirth =
                    _dateOfBirth.getFullYear().toString() +
                    "-" +
                    (_dateOfBirth.getMonth() + 1 < 10 ? "0" : "") +
                    (_dateOfBirth.getMonth() + 1).toString() +
                    "-" +
                    (_dateOfBirth.getDate() < 10 ? "0" : "") +
                    _dateOfBirth.getDate().toString();
                const _identifyDate = new Date(this.selectedEmployee.identifyDate); // ngay cap cmnd
                    this.selectedEmployee.identifyDate =
                    _identifyDate.getFullYear().toString() +
                    "-" +
                    (_identifyDate.getMonth() + 1 < 10 ? "0" : "") +
                    (_identifyDate.getMonth() + 1).toString() +
                    "-" +
                    (_identifyDate.getDate() < 10 ? "0" : "") +
                    _identifyDate.getDate().toString();               
                this.Gender = parseInt(res.data.gender); 
                this.dialogFormMode = "edit";
                this.isShowDialogDetail = true;
            }).catch(res => {
                console.log(res);
            });
        },
        /* 
        Click vao nut xoa
        CreatedBy : hmducanh (12/5/2021)
        */
        btnDelOnClick(EmployeeId, EmployeeCode) {
            this.isShowWarningDialog = true;
            this.delete_employeeId = EmployeeId;
            this.employeeCode = EmployeeCode;
            /* axios.delete("https://localhost:44332/api/v1/Employee/" + EmployeeId).then(res => {
                this.loadData();
                console.log(res);
            }).catch(res => {
                console.log(res);
            });  */
        },
        /* 
        // hien thi - an warning dialog
        // Created by : hmducanh (12/5/2021)
        */
        hideWarningDialog() {
            this.isShowWarningDialog = false;
        },  
        /* 
        nhan lenh dien form thanh cong va tao ra 1 form moi 
        => day data rong len
        created by : hmducanh (16/5/2021)
        */
        newDialog() {
            axios.get("https://localhost:44332/api/v1/Employee/maxEmployeeCode").then(res => {
                this.selectedEmployee = {};
                this.dialogFormMode = "add";
                this.isShowDialogDetail = true;
                this.selectedEmployee.employeeCode = res.data;
                this.Gender = null;
            }).catch(res => {
                console.log(res);
            });
        },   
        /* 
        hien thi dialog thong bao da thanh cong o goc tren phai man hinh
        created by : hmducanh (21/5/2021)
        */
        showNotification(message) {
            this.$notification["success"]({
                message,
                duration: 2,
            });
        },
        /* 
        nhan du lieu tu warning dialog xem co duoc xoa hay khong
        Created by : hmducanh (12/5/2021)
        */
        child_click(value)
        {
            // ktra co duoc xoa hay khong
            this.total_employee_this_page = this.employees.length;
            if(value == "0")
            {
                return ;
            }
            else
            {
                axios.delete("https://localhost:44332/api/v1/Employee/" + this.delete_employeeId).then(res => {
                console.log(res.data);
                if(this.total_employee_this_page == 1)
                {
                    this.page = this.page - 1;
                }
                else
                {
                    this.total_employee_this_page = this.total_employee_this_page - 1;
                }
                // hien thi dialog success
                    this.loadData();
                    // this.debounceShowSuccess();
                    console.log("Xoa roi")
                    this.showNotification("Xóa thành công")
                }).catch(res => {
                    console.log(res);
                });
            }
            // this.debounceShowSuccess();
        },
        /* 
        thay doi khi an vao so trang
        created by : hmducanh (13/5/2021)
        */
        onChangePage(value)
        {
            this.page = value;
            if(value < 4 || value > 17)
            {
                this.showLessItems = false;
            }
            else
            {
                this.showLessItems = true;
            }
            this.loadData();
        },
        /* 
        Thay doi so luong data tren 1 trang
        created by : hmducanh (13/5/2021)
        */
        handleChangePageSize(value)
        {
            this.pageSize = parseInt(value);
            this.page = 1;
            //console.log(this.pageSize, this.page, this.search, "check phan trang");
            this.loadData();
        },
        /* 
        Thay doi trang hien tai
        created by : hmducanh (19/05/2021)
         */
        handleChangePageIndex(value) {
            this.page = parseInt(value);
            this.loadData();
        },
        /* 
        chuyen cac dau '< >' trong phan trang ve 2 chu "truoc" va "sau"
        created by : hmducanh (120/5/2021)
        */
        itemRender(current, type, originalElement) {
            if (type === 'prev') {
                return <a class="btn-prev">Trước</a>;
            } else if (type === 'next') {
                return <a class="btn-next">Sau</a>;
            }
            return originalElement;
        },
        /* 
        chuyen doi ngay thang sang chuoi DDMMYYYY de hien thi duoc
        created by : hmducanh (11/5/2021)
        */
        formatDateDDMMYYYYnew(date) {
            var newDate = new Date(date);
            var dateString = newDate.getDate();
            var monthString = newDate.getMonth() + 1;
            var year = newDate.getFullYear();
            var s = "";
            var t = "";
            if (dateString < 10) {
                s = "0" + dateString.toString();
            } else {
                s = dateString.toString();
            }
            if (monthString < 10) {
                t = "0" + monthString.toString();
            } else {
                t = monthString.toString();
            }
            return `${s}/${t}/${year}`;
        }
    },
    data() {
        // cac bien toan cuc
        return {
            employees : [],
            departments : [],
            isShowDialogDetail: false,
            selectedEmployee: {},
            constantSelectedEmployee: {},
            dialogFormMode: "add",
            topCheckboxstatus: false,
            check_sub: false,
            validEmployee: [],
            isShowWarningDialog : false,
            delete_employeeId : "",
            isLoading : false,
            totalEmployee : 0,
            employeeCode : "",
            maxEmployeeCode : "",
            isShowSuccessDialog : false,
            showLessItems : true,
            totalRecords : 0,
            totalPage : 0,
            pageSize : 10,
            page : 1,
            search : "",
            total_employee_this_page : 0,
            
        };
    },
    watch : {},
}
</script>

<style scoped>
/* css input */
.bar { 
    display: flex;   
}
.line0 > th {
    font-size : 14px;
}
.search-table {
    height: 30px;
    width: 220px;
    border: 1px solid #bbbbbb;
    padding-left: 10px;
    padding-right: 30px;
    padding-top: 8px;
    padding-bottom: 8px;
    margin-top: 16px;
    margin-bottom: 16px;
    border-radius: 1px;
    outline: none;
    box-sizing: border-box;
    left: 200px;
    margin-left: auto;
    margin-right: 20px;
}
.search-table:hover {
    border-color: #2ca01c;
}
.refresh-button {
    background-position: -1096px -88px;
    position: relative;
    top: 20px;
    right: 10px;
}
.refresh-button:active {
    background-color: rgb(201, 201, 201);
}
.export-button {
    background-position: -704px -200px;
    position: relative;
    top: 20px;
    right: 10px;
}
.search-icon {
    background-position: -992px -360px;
    position: absolute;
    margin-top: 22px;
    right: 100px;
}
.btnDel {
    display: inline-block;
    outline: none;
    border: none;
    padding-left: 24px;
    padding-right: 24px;
    margin-left: 4px;
    height: 40px;
    width: 90px;
    line-height: 30px;
    background-color: rgb(216, 48, 48);
    border-radius: 4px;
    font-size: 13px;
    color: #ffffff;
    font-style: normal;
    cursor: pointer;
    text-align: center;
}
.btnDel:hover {
    background-color: #e73f3fb7;
}
.btnDel:active {
    background-color: #e20404;
}
input[type="checkbox"] {
    display: inline-block;
    width: 20px;
    height: 20px;
    margin-top: 10px;
    margin-left: -4%;
}
input[type="text"]:focus {
    border-color: #019160;
}
.icon-loader-show {
    display: none;
}
.hide-body {
    display: none;
}
.footer {
    position: absolute;
    bottom: 30px;
    left: 50px;
}
.disable {
    background-color: gray !important;
}
.pagging {
    align-items: center;
    display: flex;
    justify-content: space-between;
    position: absolute;
    bottom: 24px;
    height: 40px;
    width: calc(100% - 64px);
    left: 40px;
    right: 5px;
}

.right-pagging {
    display: flex;
    margin-right: 36px;
}

.select-record-number {
    margin-top: 14px;
}

.select-page {
    right: 0;
    margin-top: 16px;
    height: 20px;
    /* width: 360px; */
}


</style>