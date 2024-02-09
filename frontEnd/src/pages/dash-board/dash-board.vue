<style lang="css" src="./styles.css"></style>
  
<script lang="ts">
import MenuLateral from '../../components/MenuLateral/MenuLateral.vue'
import HeaderComponents from '../../components/HeaderComponents/HeaderComponents.vue' 
 

export default {
  components: {
    MenuLateral,
    HeaderComponents,    
}
}
</script>

<template>
    <div class="dashboard">
      <HeaderComponents />
      <div class="main-container">
        <MenuLateral />
        <router-view></router-view>
      </div>
    </div>
</template>
  
