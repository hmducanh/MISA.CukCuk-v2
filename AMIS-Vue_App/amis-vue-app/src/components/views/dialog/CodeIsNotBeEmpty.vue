<template>
    <div class="title-message-empty" v-if="open">
        Mã không được để trống.
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
}
</script>

<style scoped>
.title-message-empty {
    position: fixed;
    top: 19%;
    left: 3%;
    color : #FFFFFf;
    width: 16%;
    height: 26px;
    padding-left: 4px;
    padding-right: 4px;
    padding-top: 2px;
    padding-bottom: 2px;
    font-size: 13.2px;
    background-color: rgb(54, 54, 54);  
}

</style>