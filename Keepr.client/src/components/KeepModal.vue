<template>
    <div class="modal fade" id="keep-modal" tabindex="-1" aria-labelledby="" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered">
            <div class="modal-content">
                <div class="row  border-0 modal-header">
                    <div class="col-3">
                        <h6 class="activity-icon"><i class="mdi mdi-key-star"></i> {{ keep.kept }}</h6>
                    </div>
                    <div class="col-3">
                        <h6 class="activity-icon"><i class="mdi mdi-share-variant-outline"></i> {{ keep.shares }}</h6>
                    </div>
                    <div class="col-3">
                        <h6 class="activity-icon"><i class="mdi mdi-eye"></i> {{ keep.views }}</h6>
                    </div>
                    <div class="col-2">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-6">
                            <img class="img-fluid" :src="keep.img" alt="">
                        </div>
                        <div class="col-6 position-relative">
                            <div>
                                <h2 class="border-bottom display-4">{{ keep.name }}</h2>
                            </div>
                            <div>
                                <h6 class="text-muted"><em>
                                        {{ keep.description }}
                                    </em></h6>
                            </div>
                            <!-- NOTE consider alignment in some means other than absolute -->
                            <div class="row position-absolute bottom-0">
                                <!-- add to vault -->
                                <div class="col-3">
                                    <h6><i class="mdi mdi-key-star"></i></h6>
                                </div>
                                <!-- delete if creator -->
                                <div @click.stop="deleteKeep" class="col-2">
                                    <i class="delete-icon mdi mdi-delete-forever"></i>
                                </div>
                                <!-- creator info -->
                                <div class="col-6">
                                    <h6 class="text-break">
                                        <img @click.stop="goToProfile" class="creator-img" :src="keep?.creator?.picture"
                                            alt="">
                                        {{ keep?.creator?.name }}

                                    </h6>
                                </div>

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

export default {
    setup() {
        const router = useRouter()
        return {
            keep: computed(() => AppState.activeKeep),
            goToProfile() {
                router.push({ name: "Profile", params: { id: AppState.activeKeep.creatorId } })
                Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
            },
            async deleteKeep() {
                if (await Pop.confirm('Are you sure you want to delete this Keep?')) {
                    keepsService.deleteKeep()

                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
@import "../assets/scss/variables";

.activity-icon {
    color: $keepr-primary;
    font-size: 1.5rem;
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
</style>