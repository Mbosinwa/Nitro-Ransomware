# Nitro Ransomware - Proof of Concept
Uses discord nitro gift subscription as ransom. C# Ransomware for educational purposes only.
 
 ## About
 Ransomware is a type of malware that prevents or limits users from accessing their files in their system. It locks the user's files until the ransom is paid, in this case, 
 a discord nitro subscription. If a user wants to unlock their files, a decryption key is needed. The ransomware asks for the ransom in exchange for the decryption key. 
 
## Disclaimer
This ransomware should not be used to harm/threat/hurt others.
It's purpose is only to share knowledge and awareness about malware/cryptography/operating systems/programming.
Nitro-Ransomware is an academic ransomware made for learning and spreading awareness about how security/cryptography can be used maliciously.
 
 ## How it works
 When the .exe file is ran, it encrypts the user's documents, desktop, and pictures folder. It then recursively checks for any nested folders in the folders, and encrypts all of its
 contents. In order to decrypt the files, the user has to paste a valid Discord nitro gift subscription and submit it. The program checks if it is valid, and if it is, it is
 sent to your webhook. The user receives the decryption key which allows them to decrypt the encrypted files. If it is not, the decrypion key will not be sent, and the user wil not be able to 
 open their files.
 
 This program should only be used for educational purposes only. Do not use this on others maliciously.
 
 ## Showcase
 [showcase](https://youtu.be/eD_mG2L8G38)
 
 ## Preview
 ###### Webhook Preview
 ![Picture](https://i.ibb.co/107VhDh/Screenshot-420.png)
 
 ###### Ransomware 
 ![Picture1](https://i.ibb.co/0Dwkf7M/Screenshot-422.png)
 ## Features
 - AES encryption/decryption
 - Adds to startup registry
 - Grabs user's pc username, name, and uuid
 - Discord nitro checker 
 - Token grabber
 - IP grabber
 - Discord webhook logs
 - Disables taskmgr, registry, run, cmd and powershell scripts
## Changelog
 - Improved ransomnote
 - Changed form app icon and name
 - Fixed discord logs
 - Asks to be ran as administrator
 - Added manifest
 - Cleaned up code
 - Form is always on top
## Todo
 - Write a ransomnote file to desktop
 - Disable/encrypt other apps
 - Fix timer resetting on restart
 - Change lockscreen
 - Add more grabber locations (add me paf.#0001 if u have some good list)
 - Potentially blocking internet access for everything else but the ransomware (idk if i can do this im braindead hmu paf.#0001 if u can help or pull req)

## Usage
1. Make sure you have Visual Studi with C# installed. (.NET Desktop Development).
2. Open ```NitroRansomware.sln```, then open ```Program.cs```. 
3. Pase your webhook link next to ```WEBHOOK```.
4. You can change the decryption key too, if you want. ```DECRYPT_PASSWORD```
5. Click on release, then build the solution. Do NOT run it, because it is malware and may encrypt your files.
6. You can now test it in a protected environment such as a virtual machine.
