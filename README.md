# SwaggerTestTask
Web-Api Get/Post Test Task
Not sure in some decisions:
1. Using string value instead of FormBody, I had to use not common string value input, 
because using FormBody I had no option to catch an exeption and provide it with right error messages.
2. Using singleton MemoryCatch instead of DI, I tried to use DI, but it puzzled me, and I decided to use an easier solution.
