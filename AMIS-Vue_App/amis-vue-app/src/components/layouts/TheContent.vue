<template>
  <div class="content">
    <EmployeeList/>
  </div>
</template>
<script>

import EmployeeList from '../views/employee/EmployeeList.vue'

export default {
  name: "TheContent",
  components : {
    EmployeeList,
  },
};
</script>


<style>

</style>