<template>
    <div class="component">

    </div>
</template>


<script>
import { onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'

export default {
    setup(){

      const route = useRoute()

      onMounted(()=>{
        vaultsService.getVaultById(route.params.id)
      })

        return {
          route,
        }
    }
}
</script>


<style lang="scss" scoped>
  .masonry-frame {
    columns: 4;
  
    div {
      break-inside: avoid;
    }
  }
@media (max-width: 768px) {
  .masonry-frame {
    columns: 2;

    div {
      break-inside: avoid;
    }
  }
}
</style>