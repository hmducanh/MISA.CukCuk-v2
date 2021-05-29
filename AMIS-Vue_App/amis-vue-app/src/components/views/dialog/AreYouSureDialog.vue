<template>
    <!-- 
        Khi an nut huy thi hoi lai va dua ra su lua chon cho nguoi dung
        Created by : hmducanh (12/5/2021)
     -->
    <div id="WarningDialog" :class="{ 'warningdialog-hide': !openAreYouSure}">
        <!-- tao lop thu 2 ngan cach -->
        <div class="model"></div>
        <!--  Ve cua so  -->
        <div class="EDialog">
            <!-- logo canh bao -->
            <div class="logo-item question-icon"></div>
            <div class="line-1">Dữ liệu đã bị thay đổi. Bạn có muốn cất không?</div><br>
            <!-- <div class="line-3">__________________________________________________________________</div> -->
            <!-- button truyen trang thai co hoac khong -->
            <div style="border-top: 1px solid #b8bcc3; margin-right: 16px; margin-left: 8px; margin-top: -8px;">              
                <button class="btnNo Cancel" @click="closeAreYouSureDialog(0)">Hủy</button>
                <button class="btnNo No" @click="closeAreYouSureDialog(1)">Không</button>
                <button class="btnYes"  @click="closeAreYouSureDialog(2)">Có</button>
            </div>
        </div>

    </div>
</template>

<script>
export default ({
    props: {
        openAreYouSure: {
            type: Boolean,
            default: null,
        },
    },
    created() {
        /* 
        phím tắt để thao tác với dialog
        created by : hmducanh (20/5/2021)
        */
        document.addEventListener("keydown", function(e){
            e = e || window.event;
            // nếu nút nhận vào là enter
            if(e.keyCode == 13 && this.openAreYouSure == true) {
                this.closeAreYouSureDialog(2);
            }
            // nếu nút nhận vào là c
            if(e.keyCode == 67 && this.openAreYouSure == true)
            {
                this.closeAreYouSureDialog(1);
            }
            // nếu nút nhận vào là esc
            if(e.keyCode == 27 && this.openAreYouSure == true) {
                this.closeAreYouSureDialog(0);
            }
        }.bind(this))
    },
    methods: {
        /* 
        truyen lai thong tin len EmployeeDetail voi status la trang thai cong viec can xu ly va dong dialog nay lai
        created by : hmducanh (21/5/2021)
        */
        closeAreYouSureDialog(status)
        {
            this.$emit('status_X_button', status);
            this.$emit("closeAreYouSureDialog");
        }
    },
})
</script>


<style scoped>
.EDialog {
    padding: 24px;
    position: absolute;
    top: 46%;
    left: 44%;
    transform: translate(-50%,-50%);
    width: 460px;
    height: 190px;
    border-radius: 4px;
    background-color: #ffffff;
    padding-bottom: 80px;
}

.line-1 {
    font-size: 14px;
    font-weight: 500;
    margin-left: 0px;
}

.line-3 {
    margin-top: -32px;
    margin-left: -6px;
}

.question-icon {
    background-position: -826px -456px;
    width: 48px;
    height: 48px;
    margin-top: 16px;
    margin-left: 10px;
}

.btnYes {
    margin-top: -36px;
    margin-right: 0px;
}

.No {
    margin-top: -36px;
    margin-left: 220px;
}

.Cancel {
    margin-top: -36px;
    left: 32px;
}

</style>