<template>
    <!-- 
        Tao dialog canh bao nguoi dung khi an nut xoa
        Created by : hmducanh (12/5/2021)
     -->
    <div id="WarningDialog" :class="{ 'warningdialog-hide': !isShowWarning}">
        <!-- tao lop thu 2 ngan cach -->
        <div class="model"></div>
        <!--  Ve cua so  -->
        <div class="WDialog">
            <!-- logo canh bao -->
            <div class="logo-item warning-icon"></div>
            <div class="line-1">Bạn có thực sự muốn xóa Nhân viên {{employeeCode}}</div><br>
            <div class="line-2">không?</div>
            <!-- <div class="line-3">__________________________________________________________________</div> -->
            <!-- button truyen trang thai co hoac khong -->
            <div style="border-top: 1px solid #b8bcc3; margin-right: 20px; margin-left: 16px; margin-top: 86px;">  
                <button class="btnNo" @click="close_dialog('0')">Không</button>
                <button class="btnYes" @click="close_dialog('1')">Có</button>
            </div>
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
                this.close_dialog(1);
            }
            // an esc
            if(e.keyCode == 27 && this.isShowWarning == true){
                this.close_dialog(0);
            }
        }.bind(this))
    },
    methods: {
        /* 
        dong dialog nay
        created by : hmducanh (18/5/2021)
        */
        close_dialog(status)
        {
            this.$emit('status_delete', status);
            this.$emit("hideWarningDialog");
        }
    },
})
</script>


<style scoped>

</style>