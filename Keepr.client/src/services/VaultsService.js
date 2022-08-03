import { useRouter } from "vue-router";
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

const router = useRouter();


class VaultsService {

    async getVaultById(id) {
        
        const res = await api.get(`api/vaults/${id}`)
        logger.log("here is get by Id res", res.data)
        AppState.activeVault = res.data

    }

    findFirstKeepInVault(vaultId) {
        let found = AppState.currentProfileVaults.filter(vault => vault.id == vaultId)
        return found[0]
    }

    setFirstPicture(vaultId, imgUrl) {
        let found = this.findFirstKeepInVault(vaultId)

        found.firstPicture = imgUrl ?? "https://images.unsplash.com/photo-1577373644244-ff9935a13a2b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=742&q=80"
    }

    async deleteVault() {
        let id = AppState.activeVault.id
        const res = await api.delete(`api/vaults/${id}`)
        Pop.toast("Vault Deleted", 'success')
        router.push({ name: "Profile"})
        return res.data

    }

    async createVault(vaultData) {
        const res = await api.post('api/vaults', vaultData)
        logger.log(res.data)
        return res.data
      }
}

export const vaultsService = new VaultsService()