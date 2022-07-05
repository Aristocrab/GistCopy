<template>
    <div class="background" @click="closeModal">
        
    </div>

    <div class="modal-container">
        <div class="modal">
            <h2>Login:</h2>
            <form method="post" @submit.prevent>
                <div class="login">
                    <div class="login__username">
                        <input required v-model="username" name="username" class="login__username__input" type="text" placeholder="Username...">
                    </div>
                    <div class="login__password">
                        <input required v-model="password" name="password" class="login__password__input" type="password" placeholder="Password...">
                    </div>
                </div> 

                <div class="login__buttons">
                    <button @click="login" class="login__button gist__submit-button" type="submit">Login</button>
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
            document.querySelector(".background").style.display = "none";
            document.querySelector(".modal-container").style.display = "none";
        }, 
        openModal() {
            document.querySelector(".background").style.display = "block";
            document.querySelector(".modal-container").style.display = "flex";

            document.querySelector(".login__username__input").focus()
        }, 
        async login() {
            const response = await axios.post("https://localhost:7005/api/Users/login", {
                username: this.username,
                password: this.password,
            })
            localStorage.setItem('jwtToken', response.data)
            this.$emit('logged')
            this.username = ""
            this.password = ""
        }
    }
}
</script>

<style scoped>
    .background {
        position: absolute;
        display: none;
        z-index: 2;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.3);
    }

    .modal-container {
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

    .login {
    display: flex;
    flex-direction: column;
    width: 100%; 
}

/* Description */

.login__username {
    border-radius: 4px 4px 0 0;
    height: 48px;
    line-height: 32px;
    background-color: rgba(30, 41, 59, 0.85);
    border: var(--border);
    padding: 8px 16px 8px 16px;
}

.login__username__input {
    width: 100%;
    height: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

.login__password {
    border-radius: 0 0 4px 4px;
    height: 48px;
    line-height: 32px;
    background-color: rgba(30, 41, 59, 0.85);
    border: var(--border);
    border-top: none;
    padding: 8px 16px 8px 16px;
}

.login__password__input {
    width: 100%;
    height: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

/* Editor */
.login__text {
    height: 48px;
    line-height: 32px;
    padding: 8px 16px 8px 16px;
}

.login__text__input {
    width: 100%;
    height: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

/* Gist buttons */
.login__buttons {
    margin-top: 10px;
    display: flex;
    justify-content: right;
}

.login__button  {
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