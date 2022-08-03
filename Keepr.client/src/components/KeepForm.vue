<template>
    <div class="row flex-column">

        <form @submit.prevent="createKeep" id="keep-form">
            <div class="col">
                <label class="text-dark m-2">Keep Name: </label>
                <input v-model="editable.name" type="text"  name="name" id="name" aria-describedby="helpId" required />
            </div>
            
            <div class="col">
                <label class="text-dark m-2">Description: </label>
                <input v-model="editable.description" type="text"  name="description" id="description" aria-describedby="helpId" required />
            </div>

            <div class="col">
                <label class="text-dark m-2">Image Url: </label>
                <input v-model="editable.img" type="text"  name="img" id="img" aria-describedby="helpId" required />
            </div>
            


<!--NOTE consider a v-if="!keep" and checking if a keep with that name already exists -->
            <button class="m-1 btn btn-success" type="button " @click="">
                Create!
            </button>
            <button class="m-1 btn btn-warning" type="button " data-bs-dismiss="modal">Cancel!</button>

        </form>
    </div>
</template>


<script>
import { ref } from "@vue/reactivity"

import Pop from "../utils/Pop"

import { onMounted, watchEffect } from '@vue/runtime-core'
import { Modal } from "bootstrap"
import { useRouter } from "vue-router"
import { logger } from "../utils/Logger"

export default {
    
    setup() {
        let editable = ref({})
        const router = useRouter()
        return {
            editable,
            async createKeep() {
                try {
                    let keepData = editable.value
                    let newKeep = await keepsService.createKeep(keepData)
                    logger.log('here is the new keep', newKeep)
                    Pop.toast('Keep created', 'success')
                    Modal.getOrCreateInstance(document.getElementById('create-keep')).hide()
                    router.push({name: "Keep", params: {id: newKeep.id}})
                    editable.value = {}
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
</style>