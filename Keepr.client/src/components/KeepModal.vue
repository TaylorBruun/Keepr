<template>
    <div class="modal fade" id="keep-modal" tabindex="-1" aria-labelledby="" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-fullscreen-lg-down modal-dialog-centered">
            <div class="modal-content ">
                <div @click.stop="goToProfile" class="row  border-0 modal-header">

                    <div class=" col-7 d-flex m-1 p-1 align-items-center">
                        <img  class="selectable creator-img" :src="keep?.creator?.picture" alt="">
                        <h6 class="selectable m-2 text-break">
                            {{ keep?.creator?.name }}
                        </h6>
                    </div>
                    <div class="col-2">
                        <button type="button" title="Close" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-6">

                            <img class="img-fluid" :src="keep.img" alt="">
                            <div class="row d-flex align-items-center">
                                <!-- add to vault -->
                                <div class="col-5 me-5 m-2">
                                    <div class="btn-group dropup">
                                        <button v-if="account.id" type="button" class="btn drop-btn  dropdown-toggle"
                                            data-bs-toggle="dropdown" aria-expanded="false">
                                            Add to Vault
                                        </button>
                                        <ul class="dropdown-menu">
                                            <AddToVault v-for="v in userVaults" :key="v.id" :vault="v" />
                                        </ul>
                                    </div>
                                </div>
                                <!-- delete if creator -->
                                <div v-if="keep.creatorId == account.id" @click.stop="deleteKeep"
                                    class="col-2 selectable p-2 ms-3">
                                    <i title="Delete" class="delete-icon mdi mdi-delete">Delete</i>
                                </div>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="row p-0 border-0 modal-header">
                                <div class="col-4">
                                    <h6 class="activity-icon"><i class="mdi mdi-key-star"></i> {{ keep.kept }}</h6>
                                </div>
                                <!-- NOTE v-if false means it will never show. Enable if shares is implemented -->
                                <div v-if="false" class="col-4">
                                    <h6 class="activity-icon"><i class="mdi mdi-share-variant-outline"></i> {{
                                            keep.shares
                                    }}</h6>
                                </div>
                                <div class="col-4">
                                    <h6 class="activity-icon"><i class="mdi mdi-eye"></i> {{ keep.views }}</h6>
                                </div>
                            </div>
                            <div>
                                <h2 class="border-bottom display-4">{{ keep.name }}</h2>
                            </div>
                            <div>
                                <h6 class="text-muted"><em>
                                        {{ keep.description }}
                                    </em></h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { Modal } from 'bootstrap'
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { router } from '../router'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Pop'
import AddToVault from './AddToVault.vue'

export default {
    setup() {
        const router = useRouter();
        return {
            keep: computed(() => AppState.activeKeep),
            userVaults: computed(() => AppState.userVaults),
            account: computed(() => AppState.account),
            goToProfile() {
                router.push({ name: "Profile", params: { id: AppState.activeKeep.creatorId } });
                Modal.getOrCreateInstance(document.getElementById("keep-modal")).toggle();
            },
            async deleteKeep() {
                if (await Pop.confirm("Are you sure you want to delete this Keep?")) {
                    keepsService.deleteKeep();
                }
            }
        };
    },
    components: { AddToVault }
}
</script>


<style lang="scss" scoped>
@import "../assets/scss/variables";

.activity-icon {
    color: $keepr-primary;
    font-size: 1.3rem;
}

.creator-img {
    width: 30px;
    height: 30px;
    border-radius: 50%;
}

.delete-icon {
    color: $danger;
    font-size: 1.2rem;
}

.drop-btn {
    background-color: $keepr-secondary;
    color: rgb(246, 248, 246);
}
</style>