<template>
    <div class="title-message-empty" v-if="open" :class="{ 'warningdialog-hide': check}">
        Tên không được để trống.
    </div>
</template>

<script>
export default {
    props : {
        open: {
            typeof: Boolean,
            default: null,
        }
    },
    methods : {
    },
    data() {
        return {
            check : false,
        };
    }
}
</script>

<style scoped>
.title-message-empty {
    position: fixed;
    top: 19%;
    left: 25%;
    color : #FFFFFf;
    width: 18%;
    height: 26px;
    padding-left: 10px;
    padding-right: 10px;
    padding-top: 2px;
    padding-bottom: 2px;
    background-color: rgb(54, 54, 54);
}

</style>