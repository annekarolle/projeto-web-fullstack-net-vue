module.exports = {
    modules: [
        '@nuxtjs/toast',
    ],

    toast: {
        position: 'top-center',
        register: [
            {
                name: 'my-error',
                message: 'Oops...Something went wrong',
                options: {
                    type: 'error'
                }
            }
        ]
    }
};
