<template>

    <!-- NOTE could add very easy duplication protection by getting vaultKeeps and doing a computed to see if this vault/keep combo exists. then conditional styling like :class="{'disabled' : COMPUTEDNAME(bool for already exists)}" But is this intended behavior? Would probably ask product owner-->

    <li><a class="dropdown-item" @click="addKeepToVault"><i class="mdi mdi-key-star"></i>{{ vault.name }}</a></li>
</template>


<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { vaultKeepsService } from '../services/vaultKeepsService'
import Pop from '../utils/Pop';


export default {
    props: { vault: { type: Object, required: true } },
    setup(props) {
        return {

            // alreadyExists: computed(()=> AppState.currentKeepsByVault.filter(keep => keep.id == AppState.activeKeep.id && keep.vaultKeepId == props.vault.id)),

           
            async addKeepToVault() {
                const vaultId = props.vault.id
                const keepId = AppState.activeKeep.id
                await vaultKeepsService.keep(vaultId, keepId)
                Pop.toast("Keep added to Vault!", 'success')
                AppState.activeKeep.kept++

            }
        }
    }
}
</script>


<style lang="scss" scoped>
</style>