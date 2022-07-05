<template>
    <div class="register-background" @click="closeModal">
        
    </div>

    <div class="register-modal-container">
        <div class="modal">
            <h2>Register:</h2>
            <form method="post" @submit.prevent>
                <div class="register">
                    <div class="register__username">
                        <input required v-model="username" name="username" class="register__username__input" type="text" placeholder="Username...">
                    </div>
                    <div class="register__password">
                        <input required v-model="password" name="password" class="register__password__input" type="password" placeholder="Password...">
                    </div>
                </div> 

                <div class="register__buttons">
                    <button @click="register" class="register__button gist__submit-button" type="submit">Register</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
import axios from "axios";

export default { 
    data() {
        return {
            username: '',
            password: ''
        }
    },  
    emits: ["logged"],
    methods: {
        closeModal() {
            document.querySelector(".register-background").style.display = "none";
            document.querySelector(".register-modal-container").style.display = "none";
        }, 
        openModal() {
            document.querySelector(".register-background").style.display = "block";
            document.querySelector(".register-modal-container").style.display = "flex";
        }, 
        async register() {
            const response = await axios.post("https://localhost:7005/api/Users/register", {
                username: this.username,
                password: this.password,
            })
            localStorage.setItem('jwtToken', response.data)
            this.$emit('logged')
        }
    }
}
</script>

<style scoped>
    .register-background {
        position: absolute;
        display: none;
        z-index: 2;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.3);
    }

    .register-modal-container {
        position: absolute;
        width: 100%;
        height: 100%;
        /* display: flex; */
        display: none;
        align-items: center;
        justify-content: center;
    }

    .modal {
        z-index: 3;
        width: 500px;
        height: 250px;
        background-color: rgba(15, 23, 42, 0.85);
        border-radius: 4px;
        border: var(--border);
        padding: 16px;
    }

    .register {
    display: flex;
    flex-direction: column;
    width: 100%; 
}

/* Description */

.register__username {
    border-radius: 4px 4px 0 0;
    height: 48px;
    line-height: 32px;
    background-color: rgba(30, 41, 59, 0.85);
    border: var(--border);
    padding: 8px 16px 8px 16px;
}

.register__username__input {
    width: 100%;
    height: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

.register__password {
    border-radius: 0 0 4px 4px;
    height: 48px;
    line-height: 32px;
    background-color: rgba(30, 41, 59, 0.85);
    border: var(--border);
    border-top: none;
    padding: 8px 16px 8px 16px;
}

.register__password__input {
    width: 100%;
    height: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

/* Editor */
.register__text {
    height: 48px;
    line-height: 32px;
    padding: 8px 16px 8px 16px;
}

.register__text__input {
    width: 100%;
    height: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

/* Gist buttons */
.register__buttons {
    margin-top: 10px;
    display: flex;
    justify-content: right;
}

.register__button  {
    cursor: pointer;
    color: var(--text-color);
    display: inline-flex;
    align-items: center;
    justify-content: center;
    padding: 14px;
    height: 40px;
    font-size: 14px;
    border: var(--border);
    border-radius: 4px;
    background-color: rgba(15, 23, 42, 0.85);
}
</style>