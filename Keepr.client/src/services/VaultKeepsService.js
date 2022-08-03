import { logger } from "../utils/Logger"
import { api } from "./AxiosService"



class VaultKeepsService{

    async keep(vaultId, keepId){
        let vaultKeep = {vaultId, keepId}
        const res = await api.post('api/vaultKeeps', vaultKeep)
        logger.log("here is the new vaultKeep", res.data)
        return res.data
    }
}

export const vaultKeepsService = new VaultKeepsService()