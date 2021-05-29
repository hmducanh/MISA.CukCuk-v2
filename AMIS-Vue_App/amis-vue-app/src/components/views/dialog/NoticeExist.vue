<template>
    <!-- 
        Tao dialog de bao la ma nhan vien da ton tai trong database
        Created by : hmducanh (17/5/2021)
     -->
    <div id="WarningDialog" :class="{ 'warningdialog-hide': !isShowWarning}">
        <!-- tao lop thu 2 ngan cach -->
        <div class="model"></div>
        <!--  Ve cua so  -->
        <div class="WDialog">
            <!-- logo canh bao -->
            <div class="logo-item warning-icon"></div>
            <div class="line-1">Mã nhân viên {{employeeCode}} đã tồn tại trong hệ</div><br>
            <div class="line-2">thống, vui lòng kiểm tra lại.</div>
            <!-- <div class="line-3">_________________________________________________________________</div> -->
            <!-- button truyen trang thai co hoac khong -->
            <div style="border-top: 1px solid #b8bcc3; margin-right: 20px; margin-left: 16px; margin-top: 86px;">  </div>
            <button class="btnYes" @click="closeNoticeDialog()">Đồng ý</button>
        </div>

    </div>
</template>

<script>
export default ({
    props: {
        isShowWarning: {
            type: Boolean,
            default: null,
        },
        employeeCode: {
            type: String,
            default: null,
        }
    },
    created() {
        /* 
        phím tắt để thao tác với dialog
        created by : hmducanh (20/5/2021)
        */
        document.addEventListener("keydown", function(e){
            e = e || window.event;
            // an enter
            if(e.keyCode == 13 && this.isShowWarning == true){
                this.closeNoticeDialog();
            }
        }.bind(this))
    },
    methods: {
        /* 
        dong dialog nay
        created by : hmducanh (18/5/2021)
        */
        closeNoticeDialog()
        {
            this.$emit("closeNoticeDialog");
        }
    },
})
</script>


<style scoped>

</style>