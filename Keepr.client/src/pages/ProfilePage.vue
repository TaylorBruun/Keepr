<template>
    <div class="component">
        <div class="d-flex justify-content-between">

            <img class="m-4 profile-img" :src="profile.picture" alt="">
            <div>
                <h2 class="text-break m-4 display-2">
                    {{ profile.name }}
                </h2>
                <h5 class="m-4">Vaults: {{ profileVaults.length }} </h5>
                <h5 class="m-4">Keeps: {{ profileKeeps.length }} </h5>
            </div>
        </div>
        <div class="spacer"></div>

        <div>
            <h4 class="m-2 p-2"> Vaults:</h4>




            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <div class="masonry-frame">
                            <div v-if="account.id == profile.id" @click="" data-bs-toggle="modal"
                                data-bs-target="#create-vault" class="new-item p-3 m-3">
                                <h3>Create New Vault</h3>
                                <h6></h6>
                                <h1 class="text-center display-1"><i class="mdi mdi-plus"></i></h1>
                            </div>
                            <Vault v-for="v in profileVaults" :key="v.id" :vault="v" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="small-spacer"></div>

        <div class="">
            <h4> Keeps:</h4>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <div class="masonry-frame">

                            <div v-if="account.id == profile.id" @click="" data-bs-toggle="modal"
                                data-bs-target="#create-keep" class="new-item p-3 m-3">
                                <h3>Create New Keep</h3>
                                <h6></h6>
                                <h1 class="text-center display-1"><i class="mdi mdi-plus"></i></h1>
                            </div>

                            <Keep v-for="k in profileKeeps" :key="k.id" :keep="k" class=""></Keep>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <!-- Vault Modal -->
    <div class="modal fade" id="create-vault" tabindex="-1" role="dialog" aria-labelledby="modelTitleId"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Create a new Vault</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <VaultForm />
                </div>
                <div class="modal-footer">

                </div>
            </div>
        </div>
    </div>


    <!-- Keep Modal -->
    <div class="modal fade" id="create-keep" tabindex="-1" role="dialog" aria-labelledby="modelTitleId"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Create a new Keep</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <KeepForm />
                </div>
                <div class="modal-footer">

                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed, onMounted, watchEffect } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService'
import Keep from '../components/Keep.vue'
import Vault from '../components/Vault.vue'
import VaultForm from '../components/VaultForm.vue'
import KeepForm from '../components/KeepForm.vue'
import { keepsService } from '../services/KeepsService'

export default {
    setup() {
        const route = useRoute();
        watchEffect(() => AppState.currentProfileVaults = [...AppState.currentProfileVaults])
        watchEffect(() => AppState.homeKeeps = [...AppState.homeKeeps])

        onMounted(async () => {
            try {
                await profilesService.GetProfile(route.params.id);
                await profilesService.GetProfileKeeps(route.params.id);
                await profilesService.GetProfileVaults(route.params.id);
                AppState.currentProfileVaults.forEach(async vault => {
                    await keepsService.GetFirstPictureByVault(vault.id)
                });
            }
            catch (error) {
                Pop.toast(error, "error");
                logger.error(error);
            }
        });
        return {
            profile: computed(() => AppState.currentProfile),
            account: computed(() => AppState.account),
            profileKeeps: computed(() => AppState.currentProfileKeeps),
            profileVaults: computed(() => AppState.currentProfileVaults)
        };
    },
    components: { Keep, Vault }
}
</script>


<style lang="scss" scoped>
@import "../assets/scss/variables";

.profile-img {
    width: 200px;
    height: 200px;
    border-radius: 50%;

}

.spacer {
    height: 15vh;
}

.small-spacer {
    height: 5vh;
}

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



.new-item {
    background-color: #f0fffa;

    color: rgb(5, 5, 5);
    text-shadow: 0px 0px 6px white;
    box-shadow: 0 3px 3px -2px rgba(0, 0, 0, 0.2), 0 3px 4px 0 rgba(0, 0, 0, 0.14), 0 1px 8px 0 rgba(0, 0, 0, 0.12);
    transition: all 0.1s linear;
    border-radius: 1.5%;
    overflow-x: hidden;
    background-size: cover;
    background-repeat: no-repeat;
}

.new-item:hover {
    transform: scale(1.02);
    cursor: pointer;
    opacity: 0.9
}
</style>