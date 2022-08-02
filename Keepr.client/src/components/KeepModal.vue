<template>
    <div class="modal fade" id="keep-modal" tabindex="-1" aria-labelledby="" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" >
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="keep-modal title" >{{ keep.name }}</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-6">
                            <img class="img-fluid" :src="keep.img" alt="">
                        </div>
                        <div class="col-6">
                            <h5>{{ keep.description }}</h5>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed } from 'vue'
import { AppState } from '../AppState'

export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep)
        }
    }
}
</script>


<style lang="scss" scoped>
</style>