import { Modal } from "bootstrap"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"


class KeepsService {

  async GetAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.homeKeeps = res.data
  }

  async GetKeepsByVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    let imgUrl = res.data[0]?.img
    this.setFirstPicture(vaultId, imgUrl)
  }

  findVault(vaultId) {
    let found = AppState.currentProfileVaults.filter(vault => vault.id == vaultId)
    return found[0]
  }

  setFirstPicture(vaultId, imgUrl) {
    let found = this.findVault(vaultId)

    found.firstPicture = imgUrl ?? "https://images.unsplash.com/photo-1577373644244-ff9935a13a2b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=742&q=80"
  }
  async deleteKeep() {
    let id = AppState.activeKeep.id
    const res = await api.delete(`api/keeps/${id}`)
    AppState.homeKeeps = AppState.homeKeeps.filter(keep => keep.id != id)
    Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
    Pop.toast("Keep Deleted", 'success')
    return res.data

  }



}

export const keepsService = new KeepsService()