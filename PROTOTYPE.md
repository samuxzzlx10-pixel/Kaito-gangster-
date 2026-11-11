# Kaito — protótipo Unity

Este repositório agora contém a base de scripts C# para iniciar um protótipo mobile em terceira pessoa. Ainda não inclui uma cena Unity, modelos 3D, animações ou imagens; os visuais realistas dependem de assets e configuração gráfica adicionados ao projeto.

## Requisitos

- Unity 2022 LTS ou mais recente.
- Projeto 3D (URP recomendado para mobile).
- Pacotes TextMeshPro e Unity UI (TMP Essentials importados).
- EventSystem e GraphicRaycaster no Canvas para os controles de toque.

## Configuração da cena

1. Crie um objeto para Kaito, adicione `CharacterController`, `KaitoController`, `KaitoHealth` e `KaitoCombat`.
2. Posicione uma câmera de terceira pessoa; atribua-a em `KaitoController` e `KaitoCombat` (ou marque-a como MainCamera).
3. Crie um Canvas com `EventSystem`. Faça o joystick com duas imagens UI e adicione `VirtualJoystick` ao fundo; associe o fundo, a alavanca e o Kaito.
4. Crie um botão UI de disparo, adicione `MobileFireButton` e associe `KaitoCombat`.
5. Para HUD, crie um Slider e textos TMP, adicione `KaitoHUD` ao Canvas e associe as referências.
6. Adicione colliders aos alvos de teste. Se um alvo tiver um método público `TakeDamage(int)`, o protótipo chamará esse método ao acertá-lo.

## Controles e limitações

- Arraste o joystick para andar; a direção é relativa à câmera.
- Segure o botão de disparo para testar o raycast e o contador de munição.
- `KaitoCombat.Reload()` pode ser ligado a um botão de recarga.
- Esta é apenas uma fundação de gameplay: não há ainda mundo aberto, veículos, polícia, interiores, animações, arte realista ou build Android.
