using Sampler.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampler.ViewModels.Menu
{
            public class MenuViewModel : BaseViewModel  {
                public ObservableCollection<EffectItem> AvailableEffects { get; }
                public EffectItem SelectedEffect
                {
                    get => _selectedEffect;
                    set
                    {
                        _selectedEffect = value;
                        OnPropertyChanged();
                        LoadEffectViewModel(value);
                    }
                }

                
                
                public  readonly    ViewModel       ViewModel;
                private             EffectItem      _selectedEffect;
                public BaseViewModel CurrentEffectViewModel { get; private set; }

                public MenuViewModel( ViewModel viewModel )    {
                    ViewModel =  viewModel;
                    AvailableEffects = new ObservableCollection<EffectItem>
                    {
                        new EffectItem("Gain", () => new GainViewModel(this)),
                        new EffectItem("Sine Wave Generator", () => new SineWaveGeneratorViewModel(this)),
                        new EffectItem("Fade", () => new FadeViewModel(this)),
                        new EffectItem("To Mono", () => new ToMonoViewModel(this)),
                        new EffectItem("Mix", () => new MixViewModel(this))
                    };
                }

                private void LoadEffectViewModel(EffectItem effect)   {
                    CurrentEffectViewModel = effect.CreateViewModel();
                    OnPropertyChanged(nameof(CurrentEffectViewModel));
                }
            }

            public class EffectItem    {
                public string Name { get; }
                public Func<BaseViewModel> CreateViewModel { get; }

                public EffectItem(string name, Func<BaseViewModel> factory)  {
                    Name = name;
                    CreateViewModel = factory;
                }
            }

}
