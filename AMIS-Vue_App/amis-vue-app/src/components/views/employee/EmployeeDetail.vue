<template>
  <div
    id="EmployeeDetail"
    class="dialog"
    :class="{ 'dialog-hide': !isShow }"
  >
  <!-- tao lop thu 2 ngan cach -->
    <div class="model"></div>
    <!--  Ve cua so  -->
    <div class="dialog-content">
      <div class="dialog-header">
        <div class="dialog-title">THÔNG TIN NHÂN VIÊN</div>
        <div class="dialog-close-button" @click="btnCloseOnClick()">
          &#x2715;
        </div>
      <div class="dialog-body">
        <div class="m-row">
          <div>
            <label>Mã nhân viên(<span style="color: red">*</span>)</label>
            <input type="text">
          </div>
          <div>
            <label>Tên(<span style="color: red">*</span>)</label>
            <input type="text">
          </div>
          <div>
            <label>Ngày sinh(<span style="color: red">*</span>)</label>
            <input type="date">
          </div>
          <div>
            <label>Giới tính</label>
            <input type="checkbox" id="" value="0">
            <label>Nam</label>
            <input type="checkbox" id="" value="1">
            <label>Nữ</label>
            <input type="checkbox" id="" value="2">
            <label>Khác</label>
          </div>
        </div>
      </div>
      </div>
      <!-- footer -->
      <div class="dialog-footer">
        <button id="btnCancel" @click="btnCloseOnClick()">Hủy</button>
        <button id="btnSave" @click="btnSaveOnClick()">Lưu</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props : {
    isShow: {
        type: Boolean,
        default: false,
    },
  },
  methods : {
    // dong dialog
      btnCloseOnClick() {
        this.$emit("hideDialog");
      },
    }
}
</script>


<style scoped>
.m-row {
  width: 80%;
  display: flex;
  margin-top: 20px;
  left: 10px;
  height: 58px;
  position: relative;
}

input[type="text"],
input[type="date"] {
  height: 32px;
  width: 80%;
  border: 1px solid #bbbbbb;
  padding-left: 16px;
  padding-right: 16px;
  border-radius: 4px;
  outline: none;
  box-sizing: border-box;
}

input[type="text"]:hover {
  border-color: #019160;
}
</style>