<template>
    <div class="row flex-column">

        <form @submit.prevent="createVault" id="vault-form">
            <div class="col">
                <label class="text-dark m-2">Vault Name: </label>
                <input v-model="editable.name" type="text"  name="name" id="name" aria-describedby="helpId" required />
            </div>
            
            <div class="col">
                <label class="text-dark m-2">Description: </label>
                <input v-model="editable.description" type="text"  name="description" id="description" aria-describedby="helpId" required />
            </div>
            <div class="col">

                
                <label class="custom-control custom-checkbox">
                    <input v-model="editable.isPrivate" type="checkbox" name="private" id="private" value="checkedValue" class="custom-control-input">
                  <span class="custom-control-indicator"></span>
                  <span class="custom-control-description"></span>
                </label>
                    <label class="text-dark m-2">Private? </label>
            </div>


<!--NOTE consider a v-if="!vault" and checking if a vault with that name already exists -->
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
            async createVault() {
                try {
                    let vaultData = editable.value
                    let newVault = await vaultsService.createVault(vaultData)
                    logger.log('here is the new vault', newVault)
                    Pop.toast('Vault created', 'success')
                    Modal.getOrCreateInstance(document.getElementById('create-vault')).hide()
                    router.push({name: "Vault", params: {id: newVault.id}})
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