<div class="header" align="center">  
<img alt="Space Station 14" width="880" height="300" src="https://raw.githubusercontent.com/space-wizards/asset-dump/de329a7898bb716b9d5ba9a0cd07f38e61f1ed05/github-logo.svg">  
</div>

Space Station 14 - это ремейк SS13, который работает на [Robust Toolbox](https://github.com/space-wizards/RobustToolbox), движке, разработанном специально для этой игры на C#.

Это форк репозитория Space Station 14. Чтобы предотвратить необходимость форкать RobustToolbox, пак контента подгружается клиентом и сервером. Этот пак контента содержит всё нужное для игры на одном определённом сервере.

Если вы хотите создавать контент для SS14, вам нужен [официальный репозиторий](https://github.com/space-wizards/space-station-14). Он содержит и RobustToolbox, и контент пак для разработки новых контент паков.

## Links

<div class="header" align="center">  

[Website](https://spacestation14.com/) | [Discord](https://discord.ss14.io/) | [Forum](https://forum.spacestation14.com/) | [Mastodon](https://mastodon.gamedev.place/@spacestation14) | [Lemmy](https://lemmy.spacestation14.com/) | [Patreon](https://www.patreon.com/spacestation14) | [Steam](https://store.steampowered.com/app/1255460/Space_Station_14/) | [Standalone Download](https://spacestation14.com/about/nightlies/)  

</div>

## Documentation/Wiki

Our [docs site](https://docs.spacestation14.com/) has documentation on SS14's content, engine, game design, and more.  
Additionally, see these resources for license and attribution information:  
- [Robust Generic Attribution](https://docs.spacestation14.com/en/specifications/robust-generic-attribution.html)  
- [Robust Station Image](https://docs.spacestation14.com/en/specifications/robust-station-image.html)

We also have lots of resources for new contributors to the project.

## Contributing

We are happy to accept contributions from anybody. Get in Discord if you want to help. We've got a [list of issues](https://github.com/GreenSpaceStation/space-station-14-green/issues) that need to be done and anybody can pick them up. Don't be afraid to ask for help either!  
Just make sure your changes and pull requests are in accordance with the [contribution guidelines](https://docs.spacestation14.com/en/general-development/codebase-info/pull-request-guidelines.html).

We are not currently accepting translations of the game on our main repository. If you would like to translate the game into another language, consider creating a fork or contributing to a fork.

## Building

1. Clone this repo:
```shell
git clone https://github.com/space-wizards/space-station-14.git
```
2. Go to the project folder and run `RUN_THIS.py` to initialize the submodules and load the engine:
```shell
cd space-station-14
python RUN_THIS.py
```
3. Compile the solution:  

Build the server using `dotnet build`.

[More detailed instructions on building the project.](https://docs.spacestation14.com/en/general-development/setup.html)

## Лицензия

Весь код в этом репозитории лицензирован под [AGPLv3 лицензией](https://github.com/GreenSpaceStation/space-station-14-green/blob/master/LICENSE-AGPLv3.txt). Файл `LICENSE.TXT` сохряняется только для того, чтобы не нарушать оригинальную лицензию официальных разработчиков, и чтобы не было конфликтов при апстриме.

Большинство ассетов лицензированы под [CC-BY-SA 3.0](https://creativecommons.org/licenses/by-sa/3.0/), пока не указано иначе. Ассеты имеют свою лицензию и копирайт, указанные в метадата файлах. Например, посмотрите [метадату монтировки](https://github.com/GreenSpaceStation/space-station-14-green/blob/master/Resources/Textures/Objects/Tools/crowbar.rsi/meta.json).

> [!NOTE]
> Некоторые ассеты лицензированы под некоммерческой [CC-BY-NC-SA 3.0](https://creativecommons.org/licenses/by-nc-sa/3.0/) или похожими некоммерческими лицензиями и подлежат удалению, если вы желаете использоваться этот проект коммерчески.
