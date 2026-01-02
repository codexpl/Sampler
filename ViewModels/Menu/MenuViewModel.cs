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

                
                
                public  readonly    ViewModel       VModel;
                private             EffectItem      _selectedEffect;
                public BaseViewModel CurrentEffectViewModel { get; private set; }

                public MenuViewModel( ViewModel viewModel )    {
                    VModel =  viewModel;
                    AvailableEffects = new ObservableCollection<EffectItem>
                    {
                        new EffectItem("Gain",                  () => new GainViewModel(VModel)),
                        new EffectItem("Sine Wave Generator",   () => new SineWaveGeneratorViewModel(VModel)),
                        new EffectItem("Fade",                  () => new FadeViewModel(VModel)),
                        new EffectItem("To Mono",               () => new ToMonoViewModel(VModel)),
                        new EffectItem("Mix",                   () => new MixViewModel(VModel))
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
