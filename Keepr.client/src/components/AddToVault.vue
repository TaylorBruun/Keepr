<template>

    <!-- NOTE could add very easy duplication protection by getting vaultKeeps and doing a computed to see if this vault/keep combo exists. then conditional styling like :class="{'disabled' : COMPUTEDNAME(bool for already exists)}" But is this intended behavior? Would probably ask product owner-->
    <!-- NOTE also, there is no getAll for vaultkeeps. would be very inefficient to getKeepsByVaultId for every vault every time we click on a keep. maybe implement either a getAll for vaultkeeps or just a backend method that takes in Ids and returns a bool as to whether that is a duplicate  -->

    <li><a class="dropdown-item" @click="addKeepToVault"><i class="mdi mdi-key-star"></i>{{ vault.name }}</a></li>
</template>


<script>
import { AppState } from '../AppState';
import { vaultKeepsService } from '../services/vaultKeepsService'
import Pop from '../utils/Pop';


export default {
    props: { vault: { type: Object, required: true } },
    setup(props) {
        return {
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