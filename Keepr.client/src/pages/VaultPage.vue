<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <h2 class="display-4 m-3 p-2">{{ vault.name }}</h2>
        <h6 class="d-flex align-items-center m-3 p-2"><i class="mdi mdi-key-star"></i> {{ keeps.length }} Keeps | <i
            v-if="account.id == vault.creatorId" @click="deleteVault" title="Delete" class="delete-btn selectable delete-icon mdi mdi-delete-forever"></i>
        </h6>
        <h6 class="m-3 p-2 text-muted">{{ vault.description }}</h6>
      </div>
    </div>
  </div>
  <div class="masonry-frame">
    <Keep v-for="k in keeps" :key=k.id :keep=k />
  </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { router } from '../router'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {
  setup() {

    const route = useRoute()
    const router = useRouter()

    onMounted(async () => {
      try {
        await vaultsService.getVaultById(route.params.id)
        await keepsService.GetKeepsByVault(route.params.id)
      } catch (error) {
        router.push({ name: "Home"})
        Pop.toast("You do not have permission to view this Vault", "error");
        logger.error(error);
      }
      
    })

    return {
      route,
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.currentKeepsByVault),
      account: computed(() => AppState.account),
      async deleteVault() {
        if (await Pop.confirm('Are you sure you want to delete this Vault?')) {
          vaultsService.deleteVault()
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
@import "../assets/scss/variables";

.masonry-frame {
  columns: 4;

  div {
    break-inside: avoid;
  }
}

  @media (max-width: 1200px) {
    .masonry-frame {
      columns: 3;
  
      div {
        break-inside: avoid;
      }
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

.delete-btn {
    color: red;
    font-size: 1.5rem;
}

.icon-labels{
    color: $keepr-primary;
    font-size: 1.3rem;
}

</style>