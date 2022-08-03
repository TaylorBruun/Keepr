import { Modal } from "bootstrap"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"
import { vaultsService } from "./VaultsService"


class KeepsService {

  async GetAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.homeKeeps = res.data
  }

  async GetFirstPictureByVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    let imgUrl = res.data[0]?.img
    vaultsService.setFirstPicture(vaultId, imgUrl)
  }
  async GetKeepsByVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.currentKeepsByVault = res.data
  }


  async deleteKeep() {
    let id = AppState.activeKeep.id
    const res = await api.delete(`api/keeps/${id}`)
    AppState.homeKeeps = AppState.homeKeeps.filter(keep => keep.id != id)
    AppState.currentProfileKeeps = AppState.currentProfileKeeps.filter(keep => keep.id != id)
    Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
    Pop.toast("Keep Deleted", 'success')
    return res.data

  }

  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)
    logger.log(res.data)
    return res.data

  }

}

export const keepsService = new KeepsService()