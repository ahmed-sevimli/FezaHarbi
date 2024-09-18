# How To ML-Agents

This will be a guide for those who don't know how to use ml-agents but also want a quick start.
In order to get the best out of this guidance, the following links will prove useful.

* [Installation Guide](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Installation.md)
* [Getting Started Guide](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Getting-Started.md)
* [Training Guide](https://github.com/gzrjzcx/ML-agents/blob/master/docs/Training-ML-Agents.md)
* [Overview Page](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/ML-Agents-Overview.md)
* [Configuration Guide](https://unity-technologies.github.io/ml-agents/Training-Configuration-File/)
\*The Installation Guide is for develop branch of ml-agents, so users should be aware that it might not be stable.
If you are looking for a stable version checkout [Windows Installation Guide](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Installation-Anaconda-Windows.md).
Also note that the guide referred is deprecated.

## Installation
1. Install Unity (2023.2 or later)
2. Clone [this](https://github.com/Unity-Technologies/ml-agents.git) repo
3. Install Python between 3.10.1 - 3.10.12 (i used 3.10.11)
4. Create `venv` and install `torch`inside (You can use Conda for ease of use)
```sh
python<version> -m venv <virtual-environment-name>
```
```sh
pip install torch~=2.2.1 --index-url https://download.pytorch.org/whl/cu121
```
5. Install the `com.unity.ml-agents` Unity package using `Package Manager`
6. Install ml-agents Python packages from the local repo you cloned
```sh
pip install -e ./ml-agents-envs
```
```sh
pip install -e ./ml-agents
```

## Training
1. Go to your local repo directory
2. Run the command below:
```sh
mlagents-learn --run-id=<name-for-this-training-run>
```
- This will allow you to start training using default configuration so that you can teach your agent Reinforcement Learning.
- The resulting brain/model will be saved as an `.onnx` file in a directory called `results`


## Configuration
* In order to add customized configuration, use the command below:
```sh
mlagents-learn <trainer-config-file> --run-id=<name-for-this-training-run>
```
* If you wish to use Imitation Learning, you will need to add the demo file path in the configuration file.