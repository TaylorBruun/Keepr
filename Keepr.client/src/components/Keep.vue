<template>

    <div :style="{backgroundImage: `url(${keep.img})`}" class="keep-card p-3 m-2">
        
            <h3>{{keep.name}}</h3>
            <h6>Keeps: {{keep.kept}}| Views: {{keep.views}}</h6>
            <h6>{{keep.description}}</h6>

    </div>
    

</template>


<script>
export default {
    props: { keep: { type: Object, required: true } },
    setup(props) {
        return {

        }
    }
}
</script>


<style lang="scss" scoped>
.keep-card {
    background-color: rgb(224, 236, 238);
    color: rgb(5, 5, 5);
    box-shadow: 0 3px 3px -2px rgba(0, 0, 0, 0.2), 0 3px 4px 0 rgba(0, 0, 0, 0.14), 0 1px 8px 0 rgba(0, 0, 0, 0.12);
    transition: all 0.1s linear;
    border-radius: .5%;
    min-height: 30vh;
    background-size:cover;

    background-repeat: no-repeat;

}
.keep-card:hover {
    transform: scale(1.02);
    cursor: pointer;
    opacity: 0.9
}

.keep-img{
    
}
</style>