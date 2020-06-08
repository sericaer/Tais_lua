EVENT_DEF.GLOBAL.EVENT_TASK_COLLECT_TAX_START = 
{
    hide = true,

    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                local task = gm_data().tasks:find('COLLECT_TAX')
                gm_data().tax_expect:start(task:get_tax_rate());

            --[[for key,pop in pairs(gm_data().pops) do
                    if(pop.def.is_tax == true) then
                        pop.buffers:find("TAXED_LEVEL"..task.level).start = true;
                    end
                end]]
            end
        }
    }
}