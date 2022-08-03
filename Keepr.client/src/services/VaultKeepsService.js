import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"



class VaultKeepsService{

    async keep(vaultId, keepId){
        let vaultKeep = {vaultId, keepId}
        const res = await api.post('api/vaultKeeps', vaultKeep)
        logger.log("here is the new vaultKeep", res.data)
        return res.data
    }

    async removeFromVault(keepId){
        const foundKeep = AppState.currentKeepsByVault.find(keep => keep.id == keepId)
        await api.delete(`api/vaultKeeps/${foundKeep.vaultKeepId}`)
        foundKeep.kept--
        AppState.currentKeepsByVault = AppState.currentKeepsByVault.filter(keep => keep.id != keepId)
    }
}

export const vaultKeepsService = new VaultKeepsService()