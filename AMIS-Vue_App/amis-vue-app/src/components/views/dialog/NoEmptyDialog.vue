<template>
    <!-- 
        Dua ra dialog la "cai gi" dang bi de trong
        Created by : hmducanh (17/5/2021)
     -->
    <div id="WarningDialog" :class="{ 'warningdialog-hide': !isShowFail}">
        <!-- tao lop thu 2 ngan cach -->
        <div class="model"></div>
        <!--  Ve cua so  -->
        <div class="EDialog">
            <!-- logo canh bao -->
            <div class="logo-item fail-icon"></div>
            <div class="line-1">{{Name}}</div><br>
            <!-- <div class="line-3">_________________________________________________________________</div>
            --> <!-- button truyen trang thai co hoac khong -->
            <div style="border-top: 1px solid #b8bcc3; margin-right: 16px; margin-left: 8px;">  </div>
            <button class="btnYes" @click="closeNoEmptyDialog()">Đóng</button>
        </div>

    </div>
</template>

<script>
export default ({
    props: {
        isShowFail: {
            type: Boolean,
            default: null,
        },
        Name: {
            type: String,
            default: null,
        }
    },
    /* 
        phím tắt để thao tác với dialog
        created by : hmducanh (20/5/2021)
        */
    created() {
        document.addEventListener("keydown", function(e){
            e = e || window.event;
            // an space
            if(e.keyCode == 13 && this.isShowFail == true) {
                this.closeNoEmptyDialog();
            }
        }.bind(this))
    },
    methods: {
        closeNoEmptyDialog()
        {
            this.$emit("closeNoEmptyDialog");
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
    margin-top: -12px;
}

.line-3 {
    margin-top: -32px;
}

.fail-icon {
    background-position: -752px -462px;
    height: 36px;
    width: 36px;
    margin-top: 16px;
    margin-left: 20px;
}

.btnYes {
    left: calc(50% - 44px);
    margin-top: -36px;
}

</style>