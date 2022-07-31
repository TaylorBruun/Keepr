<template>
    <div class="component">

    </div>
</template>


<script>
export default {
    setup(){
        return {}
    }
}
</script>


<style lang="scss" scoped>
  .masonry-frame {
    columns: 4;
  
    div {
      break-inside: avoid;
    }
  }
@media (max-width: 768px) {
  .masonry-frame {
    columns: 2;

    div {
      break-inside: avoid;
    }
  }
}
</style>