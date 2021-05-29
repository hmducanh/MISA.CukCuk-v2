<template>
  <div
    id="EmployeeDetail"
    class="dialog"
    @keydown.shift.esc.prevent.stop="btnCloseXOnClick" 
    @keydown.ctrl.83.prevent.stop.exact="btnSaveOnClick" 
    @keydown.ctrl.shift.83.prevent.stop="btnSaveAndAddOnClick"
    tabindex="0"
  >
  <!-- tao lop thu 2 ngan cach -->
    <div class="model"></div>
    <!--  Ve cua so  -->
    <div class="dialog-content">
      <div class="dialog-header">
        <div style="display: flex;">
          <div class="dialog-title">THÔNG TIN NHÂN VIÊN</div>
          <div class="isCustomer">
            <input type="checkbox" class="doNothing">
            <div class="textIsEntity">Là khách hàng</div>
          </div>
          <div class="isSupplier">
            <input type="checkbox" class="doNothing">
            <div class="textIsEntity">Là nhà cung cấp</div>
          </div>
        </div>
        <div class="more-info-item">
          <div class="logo-item nav-item-19">
            
          </div>
        </div>
        <!-- nut dong dialog -->
        <div class="dialog-close-button" @click="btnCloseXOnClick()" title="Đóng ( Shift + S )">
          &#x2715;
        </div>
        <!-- cac input trong class -->
      <div class="dialog-body">
        <div class="m-row row-1">
          <div>
            <label class="title">Mã(<span style="color: red">*</span>)</label><br>
            <input type="text" ref="employeeCode" class="txtEmployeeCode" @mouseover="code_event_hover" @mouseleave="code_event_leave" v-model="employee.employeeCode" :class="{ 'error-input': !isCheckEmployeeCode,  'error-input': isExistEmployeeCode}" @input="input_Code($event)">
            <!-- <div :class="{ 'hide-msg': isCheckEmployeeCode}"><span style="font-size: 10px; color:red;"> {{msg_empty_employeeCode}} </span></div>
            <div :class="{ 'hide-msg': !isExistEmployeeCode}"><span style="font-size: 12px; color:red;"> {{msg_EmployeCodeExist}} </span></div> -->
            <CodeIsNotBeEmpty
                :open="open_code_title_empty"
                />
          </div>
          <div>
            <label label class="title">Tên(<span style="color: red">*</span>)</label><br>
            <input type="text" class="txtFullName" @mouseover="name_event_hover" @mouseleave="name_event_leave" v-model="employee.fullName" :class="{ 'error-input': !isCheckFullName}" @input="input_Name($event)">
          <!--   <div :class="{ 'hide-msg': isCheckFullName}"><span style="font-size: 12px; color:red;"> {{msg_empty_fullName}} </span></div>
           -->
              <NameIsNotBeEmpty
                :open="open_name_title_empty"
                />
           </div>
          <div>
            <label label class="title">Ngày sinh</label><br>
            <a-date-picker :placeholder="dateFormat" class="dateDateOfBirth" :format="dateFormat" v-model="employee.dateOfBirth"/>
            <!-- <input type="date" class="dateDateOfBirth" v-model="employee.dateOfBirth"> -->
          </div>
          <div class="GenderContainer">
            <label class="title" style="margin-left: 20px;">Giới tính</label><br>
            <div>
              <a-radio-group v-model="employee.gender" class="cover-gender">
                <a-radio :value="0">
                  Nam
                </a-radio>
                <a-radio :value="1">
                  Nữ
                </a-radio>
                <a-radio :value="2">
                  Khác
                </a-radio>
              </a-radio-group>
            </div>
          </div>
        </div>
        <div class="m-row row-2">
          <div>
            <label label class="title">Đơn vị(<span style="color: red">*</span>)</label><br>

            <a-select
              id="comboboxDepartment"
              show-search
              v-model="employee.departmentName" 
              placeholder="Chọn đơn vị"
              option-filter-prop="children"
              :filter-option="filterOption"
              @change="onChangeDepartment"
              :title="title_empty_departmentName"
            >
            <a-select-option v-for="department in departments" :key='department.departmentId'>{{department.departmentName}}</a-select-option>
            </a-select>
            <!--  <select name="" id="comboboxDepartment" v-model="employee.departmentName" :class="{ 'error-input': !isCheckDepartment}">
              <option v-for="department in departments" :key='department.departmentId'>{{department.departmentName}}</option>
              
            </select>  -->
          <!--   <div :class="{ 'hide-msg': isCheckDepartment}"><span style="font-size: 12px; color:red;"> {{msg_empty_departmentName}} </span></div>
           -->
          </div>
          <div>
            <label label class="title">Số CMND</label><br>
            <input type="text" class="txtIdentifyNumber" v-model="employee.identifyNumber">
          </div>
          <div>
            <label label class="title">Ngày cấp</label><br>
            <a-date-picker :placeholder="dateFormat" class="dateIdentifyDate" :format="dateFormat" v-model="employee.identifyDate"/>     
            <!-- <input type="date" class="dateIdentifyDate" v-model="employee.identifyDate"> -->
          </div>
        </div>
        <div class="m-row row-3">
          <div>
            <label label class="title">Chức danh</label><br>
            <input type="text" class="txtPositionName" v-model="employee.positionName">
          </div>
          <div>
            <label label class="title">Nơi cấp</label><br>
            <input type="text" class="txtIdentifyPlace" v-model="employee.identifyPlace">
          </div>
        </div>
        <div class="m-row row-4">
          <div>
            <label label class="title">Địa chỉ</label><br>
            <input type="text" class="txtAddress" v-model="employee.address">
          </div>
        </div>
        <div class="m-row row-5">
          <div>
            <label label class="title">ĐT di động</label><br>
            <input type="text" class="txtPhoneNumber" v-model="employee.phoneNumber" :class="{ 'error-input': !isCheckPhoneNumber}">
            <div :class="{ 'hide-msg': isCheckPhoneNumber}"><span style="font-size: 12px; color:red;"> {{msg_phoneNumber}} </span></div>
          </div>
          <div>
            <label label class="title">ĐT cố định</label><br>
            <input type="text" class="txtConstantPhoneNumber" v-model="employee.constantPhoneNumber" :class="{ 'error-input': !isCheckConstantPhoneNumber}">
            <div :class="{ 'hide-msg': isCheckConstantPhoneNumber}"><span style="font-size: 12px; color:red;"> {{msg_phoneNumber}} </span></div>
          </div>
          <div>
            <label label class="title">Email</label><br>
            <input type="text" class="txtEmail" v-model="employee.email" :class="{ 'error-input': !isCheckEmail}">
            
            <!-- <div :class="{ 'hide-msg': isCheckEmail}"><span style="font-size: 12px; color:red;"> {{msg_email}} </span></div>
           -->
          </div>
        </div>
        <div class="m-row row-6">
          <div>
            <label label class="title">Tài khoản ngân hàng </label><br>
            <input type="text" class="txtBankAccount" v-model="employee.bankAccount" :class="{ 'error-input': !isCheckBankAccount}">
            <div :class="{ 'hide-msg': isCheckBankAccount}"><span style="font-size: 12px; color:red;"> {{msg_bankAccount}} </span></div>
          </div>
          <div>
            <label label class="title">Tên ngân hàng</label><br>
            <input type="text" class="txtBankName" v-model="employee.bankName">
          </div>
          <div>
            <label label class="title">Chi nhánh</label><br>
            <input type="text" class="txtBankBranch" v-model="employee.bankBranch">
          </div>
        </div>
      </div>
      </div>
      <!-- footer -->
      <div class="dialog-footer">
        <button id="btnCancel" @click="btnCloseOnClick()">Hủy</button>
        <button id="btnCache" title="Cất (Ctrl + S)" @click="btnSaveOnClick()">Cất</button>
        <button id="btnSave" title="Cất và thêm (Ctrl + Shift + S)" @click="btnSaveAndAddOnClick()">Cất và thêm</button>
      </div>
    </div>
    <NoticeExist
    :isShowWarning="isOpenNoticeExist"
    :employeeCode="employeeCode"
    @closeNoticeDialog="closeNoticeDialog"
    />
    <NoEmptyDialog
    :isShowFail="isShowFail"
    :Name="Name"
    @closeNoEmptyDialog="closeNoEmptyDialog"
    />
    <AreYouSureDialog
    :openAreYouSure="openAreYouSure"
    @status_X_button="status_X_button"
    />
  </div>
</template>

<script>
import axios from 'axios';
import NoticeExist from './NoticeExist.vue'
import NoEmptyDialog from './NoEmptyDialog.vue'
import AreYouSureDialog from './AreYouSureDialog.vue'
import NameIsNotBeEmpty from './NameIsNotBeEmpty.vue'
import CodeIsNotBeEmpty from './CodeIsNotBeEmpty.vue'

export default {
  components : {
    NoticeExist,
    NoEmptyDialog,
    AreYouSureDialog,
    NameIsNotBeEmpty,
    CodeIsNotBeEmpty,
  },
  props : {
    employee: {
        type: Object,
        default: null,
    },
    constantEmployee: {
        type: Object,
        default: null,
    },
    formMode: {
      type: String,
      default: "add",
    },
    departments: {
      type: Array,
      default: null,
    },
    Gender: {
      type: Number,
      default: null,
    }
  },
  created() {
    // block ctrl S de trinh duyet nhan lenh ctrl S cua dev
    document.addEventListener("keydown", function (e) {
    e = e || window.event;//Get event
    if (e.ctrlKey) {
        var c = e.which || e.keyCode;//Get key code
        switch (c) {
            case 83://Block Ctrl+S
                e.preventDefault();     
                e.stopPropagation();
            break;
        }
    }
}
);
  },
  /* 
  auto focus vao o Mã
  created by : hmducanh (18/05/2021)
  */
  mounted:function() {
        this.$refs.employeeCode.focus();
      },
  methods : {
    /* 
    lien tuc nhan su kien trong o Ma
    created by : hmducanh (21/5/2021)
    */
    input_Code(event)
    {
      if(event.target.value == null || (event.target.value != null && event.target.value.trim() == ""))
      {
        if(!document.getElementsByClassName("txtEmployeeCode")[0].classList.contains("error-input"))
          document.getElementsByClassName("txtEmployeeCode")[0].classList.add("error-input");
        this.isCheckEmployeeCode = false;
      }
      else
      {
        if(document.getElementsByClassName("txtEmployeeCode")[0].classList.contains("error-input"))
          document.getElementsByClassName("txtEmployeeCode")[0].classList.remove("error-input");
        this.isCheckEmployeeCode = true;
        this.open_code_title_empty = false;
      }
    },
    /* 
    di chuot vao trong o nhap Ma
    created by : hmducanh (21/5/2021)
    */
    code_event_hover() {
      if(this.isCheckEmployeeCode == false)
        this.open_code_title_empty = true;
      else
        this.open_code_title_empty = false;
    },
    /* 
    di chuot ra ngoai o nhap Ma
    created by : hmducanh (21/5/2021)
    */
    code_event_leave() {
       this.open_code_title_empty = false;
    },
    /* 
    nhan data lien tuc tu input FullName de kiem tra co rong khong va tra ve loi
    created by : hmducanh (19/5/2021)
    */
    input_Name(event) 
    {
      if(event.target.value == null || (event.target.value != null && event.target.value.trim() == ""))
      {
        this.isCheckFullName = false;
      }
      else
      {
        this.isCheckFullName = true;
        this.open_name_title_empty = false;
      }
    },
    /* 
    di chuot vao trong o ho ten
    created by : hmducanh (21/5/2021)
    */
    name_event_hover() {
      if(this.isCheckFullName == false)
        this.open_name_title_empty = true;
      else
        this.open_name_title_empty = false;
    },
    /* 
    di chuot ra ngoai o ho ten
    created by : hmducanh (21/5/2021)
    */
    name_event_leave() {
       this.open_name_title_empty = false;
    },
      /* 
      mac dinh lai cac ham kiem tra
      created by : hmducanh (12/5/2021)
      */
      refresh()
      {
          this.isExistEmployeeCode = false;
          this.isCheckFullName = true;
          this.isCheckEmployeeCode = true;
          this.isCheckDepartment = true;
          this.title_empty_departmentName = "";
          this.isCheckBankAccount = true;
          this.isCheckPhoneNumber = true;
          this.isCheckConstantPhoneNumber = true;
          this.isCheckEmail = true;
      },
      /* 
      dong dialog va set cac bien kiem tra ve true
      created by : hmducanh (9/5/2021)
      */
      btnCloseOnClick() {
        this.refresh();
        this.$emit("hideDialog");
      },
      /* 
      khi an vao button X o goc tren trai man hinh
      kiem tra xem du lieu co bi thay doi khong
      khong => dong dialog
      co => mo hop thoai thong bao
      created by : hmducanh (17/05/2021)
      */
      btnCloseXOnClick() {
        if(this.formMode == "add")
        {
          console.log(this.employee);
          if(JSON.stringify(this.constantEmployee) === JSON.stringify(this.employee))
          {
            this.btnCloseOnClick();
          }
          else
          {
            this.openAreYouSure = true;
          }
        }
        else
        {
          axios.get("https://localhost:44332/api/v1/Employee/GetById/" + this.employee.employeeId).then(res => {
            this.compareEmployee = res.data;
            const _dateOfBirth = new Date(this.compareEmployee.dateOfBirth); // ngay sinh
                    this.compareEmployee.dateOfBirth =
                    _dateOfBirth.getFullYear().toString() +
                    "-" +
                    (_dateOfBirth.getMonth() + 1 < 10 ? "0" : "") +
                    (_dateOfBirth.getMonth() + 1).toString() +
                    "-" +
                    (_dateOfBirth.getDate() < 10 ? "0" : "") +
                    _dateOfBirth.getDate().toString();
            const _identifyDate = new Date(this.compareEmployee.identifyDate); // ngay cap cmnd
                    this.compareEmployee.identifyDate =
                    _identifyDate.getFullYear().toString() +
                    "-" +
                    (_identifyDate.getMonth() + 1 < 10 ? "0" : "") +
                    (_identifyDate.getMonth() + 1).toString() +
                    "-" +
                    (_identifyDate.getDate() < 10 ? "0" : "") +
                    _identifyDate.getDate().toString();     
            if(JSON.stringify(this.employee) === JSON.stringify(this.compareEmployee))
            {
              this.btnCloseOnClick();
            }
            else
            {
              this.openAreYouSure = true;
            }
            }).catch(err => {
              console.log(err.response.data.devMsg);
            });
        }
      },
      /* 
      neu truoc do ma o don vi bi boi mau do?, sau do nhan combobox thanh cong => remove class border : red
      created by : hmducanh (18/5/2021)
      */
      onChangeDepartment() {
        document.getElementsByClassName("ant-select-selection")[1].classList.remove("error-input");
        this.isCheckDepartment = true;
        this.title_empty_departmentName = "";
      },
      /* 
      xử lý search tại combobox
      created by : hmducanh (18/05/2021)
      */
      filterOption(input, option) {
        return (
          option.componentOptions.children[0].text.toLowerCase().indexOf(input.toLowerCase()) >= 0
        );
      },
      /* 
      chuyen tu id đơn vị => tên đơn vị
      created by : hmducanh (18/05/2021)
      */
      convert_id_to_name_department(id) {
          for(let i in this.departments)
          {
            if(this.departments[i].departmentId == id)
              return this.departments[i].departmentName;
          }
          return id;
      },
      /* 
      check su hop le cua email
      created by : hmducanh (12/5/2021)
      */
      check_valid_email(s)
      {
          let index1 = 0, index2 = 0, cnt = 0;
          for(let i in s)
          {
              if(s[i] == '@')
                  index1 = i;
              if(s[i] == '.')
              {
                  cnt++;
                  if(cnt > 1)
                  {
                    // khong cho 2 dau '.' lien nhau
                    if(i - index2 == 1)
                      return false;
                  }
                  index2 = i;
              }
              //ky tu hop le
              if((s[i] >= '0' && s[i] <= '9') || (s[i] >= 'a' && s[i] <= 'z') || (s[i] >= 'A' && s[i] <= 'Z') || s[i] == '@' || s[i] == '.')
              {
                  //ok
              }
              else
              {
                  return false;
              }
          }
          // @ khong dung dau cau va giua @ va '.' phai co ky tu la '.' khong dung cuoi
          if((index1 > 0) && (index2 - index1 > 1) && (index2 != s.length - 1))
              return true;
          return false;
      },
      /* 
      kiem tra co trung ma nhan vien da ton tai trong database khong
      */
      async checkExistEmployeeCode()
      {
        await axios.get("https://localhost:44332/api/v1/Employee/" + this.employee.employeeCode).then(res => {
                this.isExistEmployeeCode = res.data;   
          }).catch(err => {
            console.log(err.response.data.devMsg);
          });
          console.log(this.isExistEmployeeCode, "exist");
        return this.isExistEmployeeCode;
      },
      /* 
      kiem tra xem du lieu co hop le khong truoc khi gui len server
      created by : hmducanh (10/5/2021)
      */
      check_validate()
      {
        // kiem tra du lieu
        this.check = true;
        // 1. Khong duoc de trong cac o bat buoc nhap
        // 1.1 Don vi de trong
        if(this.employee.departmentName == null || (this.employee.departmentName != null && this.employee.departmentName.trim() == ""))
        {
          this.check = false;
          this.isCheckDepartment = false;
          this.title_empty_departmentName = this.msg_empty_departmentName;
          document.getElementsByClassName("ant-select-selection")[1].classList.add("error-input");

          // document.querySelector(".ant-select-selection").classList.add("error-input");
          this.Name = "Đơn vị không được để trống.";
        }
        else
        {
          document.getElementsByClassName("ant-select-selection")[1].classList.remove("error-input");
          this.isCheckDepartment = true;
          this.title_empty_departmentName = "";
        }
        // 1.2 Ten de trong
        if(this.employee.fullName == null || (this.employee.fullName != null && this.employee.fullName.trim() == ""))
        {
          this.check = false;
          this.isCheckFullName = false;
          this.Name = "Tên không được để trống.";
        }
        else
        {
          this.isCheckFullName = true;
        }
        // 1.3 Ma nhan vien trong
        if(this.employee.employeeCode == null || (this.employee.employeeCode != null && this.employee.employeeCode.trim() == ""))
        {
          this.check = false;
          this.isCheckEmployeeCode = false;
          this.isExistEmployeeCode = false;
          this.Name = "Mã không được để trống.";
          if(!document.getElementsByClassName("txtEmployeeCode")[0].classList.contains("error-input"))
          document.getElementsByClassName("txtEmployeeCode")[0].classList.add("error-input");
        }
        else
        {
          if(document.getElementsByClassName("txtEmployeeCode")[0].classList.contains("error-input"))
          document.getElementsByClassName("txtEmployeeCode")[0].classList.remove("error-input");
          this.isCheckEmployeeCode = true;
        }
        /*
        // 2. kiem tra so dien thoai
        for(let i in this.employee.phoneNumber)
        {
          if(isNaN(this.employee.phoneNumber[i]))
          {
            this.isCheckPhoneNumber = false;
            this.check = false;
            this.Name = "Số điện thoại không đúng định dạng.";
            break;
          }
        }
        for(let i in this.employee.constantPhoneNumber)
        {
          if(isNaN(this.employee.constantPhoneNumber[i]))
          {
            this.isCheckConstantPhoneNumber = false;
            this.check = false;
            this.Name = "Số điện thoại cố định không đúng định dạng.";
            break;
          }
        }
        // 3. kiem tra tai khoan ngan hang
        for(let i in this.employee.bankAccount)
        {
          if(isNaN(this.employee.bankAccount[i]))
          {
            this.isCheckBankAccount = false;
            this.check = false;
            this.Name = "Tài khoản ngân hàng không đúng định dạng.";
            break;
          }
        }
        // 4. kiem tra email hop le
        if(this.check_valid_email(this.employee.email) == false)
        {
          this.isCheckEmail = false;
          this.check = false;
          this.Name = "Email không hợp lệ";
        }
        else
        {
          this.isCheckEmail = true;
        } */
        return this.check;
      }, 
      /* 
      hien thi dialog thong bao khi da them thanh cong
      created by : hmducanh (21/5/2021)
      */
      showNotification(message) {
            this.$notification["success"]({
                message,
                duration: 2,
            });
      },
      /* 
      an vao Cất button 
      Gui data len server
      created by : hmducanh (10/05/2021) 
      */
      async btnSaveOnClick() {
        // goi ham kiem tra du lieu
          if(this.check_validate() == false)
          {
            this.isShowFail = true;
            return ;
          } 
        // chuyen dinh dang "don vi" tu id ve name
        this.employee.departmentName = this.convert_id_to_name_department(this.employee.departmentName);
        // check formmode xem la sua hay nhap moi
        if(this.formMode == "add")
        {
          // kiem tra ma nhan vien da ton tai trong database chua
          this.isExistEmployeeCode = false;
          this.employeeCode = this.employee.employeeCode;
          if(await this.checkExistEmployeeCode() == true)
          {
            this.isOpenNoticeExist = this.isExistEmployeeCode;
            // console.log(this.isExistEmployeeCode );
            return ;
          }
          axios.post("https://localhost:44332/api/v1/Employee", this.employee).then(res => {
          console.log(res, "them thanh cong");
          this.showNotification("Thêm thành công")
          this.refresh();
          this.$emit("hideDialog");
          }).catch(err => {
            console.log(err.response.data.devMsg);
          });
        }
        if(this.formMode == "edit")
        {
          console.log("Them nhan vien", this.employee)
          axios.put("https://localhost:44332/api/v1/Employee", this.employee).then(res => {
          console.log(res, "update thanh cong");
          this.showNotification("Sửa thành công")
          this.refresh();
          this.$emit("hideDialog");
          }).catch(err => {
            // dialog trung put
            console.log(err.response.data.devMsg);
          });
        }
      },
      /* 
      an vao Cat va Them button
      Gui data len va tao ra 1 form moi
      created by : hmducanh (16/5/2021)
      */
     async btnSaveAndAddOnClick() {
        // goi ham kiem tra du lieu
         if(this.check_validate() == false)
        {
          this.isShowFail = true;
          return ;
        } 
        // chuyen dinh dang "don vi" vi id ve name
        this.employee.departmentName = this.convert_id_to_name_department(this.employee.departmentName);
        // check formmode xem la sua hay nhap moi
        if(this.formMode == "add")
        {
          // kiem tra ma nhan vien da ton tai trong database chua
          this.isExistEmployeeCode = false;
          this.employeeCode = this.employee.employeeCode;
          if(await this.checkExistEmployeeCode() == true)
          {
            this.isOpenNoticeExist = this.isExistEmployeeCode;
            // console.log(this.isExistEmployeeCode );
            return ;
          }
          // console.log("Dang o cat va them");
          axios.post("https://localhost:44332/api/v1/Employee", this.employee).then(res => {
          console.log(res, "them thanh cong va chuan bi tao dialog moi");
          this.showNotification("Thêm thành công");
          this.refresh();
          this.$emit("newDialog");
          }).catch(err => {
            console.log(err.response.data.devMsg);
          });
        }
        if(this.formMode == "edit")
        {
          axios.put("https://localhost:44332/api/v1/Employee", this.employee).then(res => {
          console.log(res, "update thanh cong");
          this.showNotification("Sửa thành công");
          this.refresh();
          this.$emit("newDialog");
          }).catch(err => {
            console.log(err.response.data.devMsg);
          });
        }
      },
      /* 
      dong noticedialog
      created by : hmducanh (17/05/2021)
      */
      closeNoticeDialog()
      {
        this.isOpenNoticeExist = false;
      },
      /* 
      dong dialog kiem tra co ro~ng hay khong
      created by : hmducanh (17/05/2021)
      */
      closeNoEmptyDialog()
      {
        this.isShowFail = false;
      },
      /* 
      dong dialog kiem tra xem da chac chan chua
      created by : hmducanh (17/05/2021)
      */
      closeAreYouSureDialog()
      {
        this.openAreYouSure = false;
      },
      /* 
      nhan va xu ly du lieu duoc gui ve tu AreYouSureDialog
      created by : hmducanh (17/05/2021)
      */
      status_X_button(status)
      {
        // an vao huy
        if(status == 0)
        {
          // console.log(status);
          this.openAreYouSure = false;
        }
        // an vao khong
        else if(status == 1)
        {
          this.openAreYouSure = false;
          this.btnCloseOnClick();
          //console.log(status,  this.openAreYouSure, "??");
        }
        // an vao co
        else if(status == 2)
        {
          this.openAreYouSure = false;
          this.btnSaveOnClick();
          //console.log(status);
        }
      }
    },
  data() {
    return {
      isCheckEmployeeCode : true,
      isCheckDepartment : true,
      isCheckFullName : true,
      isCheckPhoneNumber : true,
      isCheckConstantPhoneNumber : true,
      isCheckBankAccount : true,
      isCheckEmail : true,
      isExistEmployeeCode : false,
      check : true,
      customGender : this.Gender,
      employeeCode : "",
      isOpenNoticeExist : false,
      isShowFail : false,
      openAreYouSure : false,
      Name : "",
      compareEmployee : {},
      dateFormat: 'DD/MM/YYYY',
      msg_empty_employeeCode : "Mã nhân viên không được để trống !",
      msg_empty_fullName : "Tên nhân viên không được để trống !",
      msg_empty_departmentName : "Đơn vị không được để trống !",
      msg_phoneNumber : "Số điện thoại không hợp lệ !",
      msg_email : "Email không hợp lệ !",
      msg_bankAccount : "Tài khoản ngân hàng không hợp lệ !",
      msg_EmployeCodeExist: "Mã nhân viên đã tồn tại !",
      title_empty_departmentName : "",
      open_code_title_empty : false,
      open_name_title_empty : false,
    };
  }
}
</script>


<style scoped>
input[type="text"],
input[type="date"] {
  height: 32px;
  border: 1px solid #bbbbbb;
  padding-left: 10px;
  padding-right: 16px;
  padding-bottom: 3px;
  border-radius: 1px;
  outline: none;
  box-sizing: border-box;
  margin-top: 4px;
}
input:focus {
  border-color: #2ca01c;
}

.nav-item-19 {
  position: absolute;
  right: 34px;  
  top: 10px;
  background-position: -88px -145px;
}
.cover-gender {
  position: absolute;
  right: 70px;
  top: 28px;
  border-radius: 1px;
}
.cover-gender:focus {
  border-color: #2ca01c;
}
.title {
  font-weight: bold;
}
.hide-msg {
  display: none;
}

.error-input {
    border:1px solid red !important;
}

</style>