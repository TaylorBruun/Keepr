<template>
  <div class="masonry-frame m-2 p-2">

    <Keep v-for="k in keeps" :key=k.id :keep=k />
  </div>

</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'

export default {
  name: 'Home',
  setup() {
    watchEffect(() => AppState.homeKeeps = [...AppState.homeKeeps])
    onMounted(async () => {
      try {
        await keepsService.GetAllKeeps()
      } catch (error) {
        Pop.toast(error, "error")
        logger.error(error)
      }
    })
    return {
      keeps: computed(() => AppState.homeKeeps)
    }
  }
}
</script>

<style scoped lang="scss">
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
