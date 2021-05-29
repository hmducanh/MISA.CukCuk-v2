<template>
    <div>
        <transition>
            <div class="dialog" v-if="open" style="display: flex;">
                <div class="logo-item success-icon"></div>
                <div class="msg">Đã xóa nhân viên thành công</div>
            </div>
        </transition>
    </div>
</template>

<script>


export default {
    props: ['open'],
    methods : {
        
    }
};
</script>

<style scoped>
.dialog {
    position: fixed;
    top: 11.2%;
    left: 30%;
    background: #fff;
    width: 18vw;
    left: 13vw;
    padding: 20px;
    background-color: #e6e6f5;
}

.success-icon {
    background-position: -984px -456px;
    width: 48px;
    height: 48px;
    margin-top: -10px;
    margin-bottom: -10px;
}
.msg {
    margin-top: 2%;
    font-size: 14px;
}

.v-enter-from {
    transform: translateY(-200px);
}
.v-enter-to {
    transform: translateY(0px);
}
.v-enter-active {
    transition: transform 0.5s;
}
.v-leave-from {
    transform: translateY(0px);
}
.v-leave-to {
    transform: translateY(-200px);
}
.v-leave-active {
    transition: transform 0.5s;
}
.backdrop-enter-from,
.backdrop-leave-to {
    opacity: 0;
}
.backdrop-enter-to,
.backdrop-leave-from {
    opacity: 1;
}
.backdrop-enter-active,
.backdrop-leave-active {
    transition: opacity 0.5s;
}
</style>